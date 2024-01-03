import * as THREE from 'three';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { PointerLockControls } from 'three/addons/controls/PointerLockControls.js';

THREE.Cache.enabled = true;

let renderContainer, blocker, instructions, loadingContainer, loadingProgress, loadingProgressLabel, roomContainer, roomInfo, errorContainer,
    optionsContainer;
let camera, controls, scene, renderer, raycaster, loader;
let hasFocus, selectedBuilding;

let textures = {};
let objects = [];
let moveForward = false;
let moveBackward = false;
let moveLeft = false;
let moveRight = false;
let moveUp = false;
let moveDown = false;

let direction = new THREE.Vector3();
let velocity = new THREE.Vector3();

let prevTime = performance.now();

// Spuštění
window.addEventListener("load", () => {
  init();
  animate();
});

// Inicializace
function init() {
  // HTML Elementy
  renderContainer = document.getElementById('renderer');
  blocker = document.getElementById( 'blocker' );
  instructions = document.getElementById( 'instructions' );
  loadingContainer = document.getElementById('loadingContainer');
  loadingProgress = document.getElementById('loadingProgress');
  loadingProgressLabel = document.getElementById('loadingProgressLabel');
  roomContainer = document.getElementById('roomContainer');
  roomInfo = document.getElementById('roomInfo');
  errorContainer = document.getElementById('errorContainer');
  optionsContainer = document.getElementById('optionsContainer');
  
  // Nastavení kamery
  camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
  camera.position.set(0, 40, 4);
  
  // Nastavení světla pro celou mapu (nejsou stíny)
  const light = new THREE.AmbientLight(0xffffff);
  
  // Nastavení scény
  scene = new THREE.Scene();
  scene.add(light);
  scene.background = textures['pozadi'];

  // Nastavení mapování myši pro interakci s objekty
  raycaster = new THREE.Raycaster();
  
  // Renderer
  renderer = new THREE.WebGLRenderer();

  renderer.setPixelRatio(window.devicePixelRatio);
  canvasResize();
  renderer.render(scene, camera);
  renderContainer.appendChild(renderer.domElement);
  
  // Nastavení ovládání
  controls = new PointerLockControls(camera, renderer.domElement);

  instructions.addEventListener('click', () => {
    controls.lock();
  }, false);

  controls.addEventListener('lock', () => {
    blocker.style.display = 'none';
    roomContainer.style.display = 'none';
    renderContainer.addEventListener('click', dotek);
  });

  controls.addEventListener('unlock', () => {
    blocker.style.display = 'block';
    renderContainer.removeEventListener('click', dotek);
  });

  scene.add(controls.getObject());

  const onKeyDown = function (event) {
    switch (event.code) {
      case 'ArrowUp':
      case 'KeyW':
        moveForward = true;
        break;

      case 'ArrowLeft':
      case 'KeyA':
        moveLeft = true;
        break;

      case 'ArrowDown':
      case 'KeyS':
        moveBackward = true;
        break;

      case 'ArrowRight':
      case 'KeyD':
        moveRight = true;
        break;

      case 'Space':
        moveUp = true;
        break;
  
      case 'KeyC':
        moveDown = true;
        break;
    }
  };

  const onKeyUp = function (event) {
    switch (event.code) {
      case 'ArrowUp':
      case 'KeyW':
        moveForward = false;
        break;

      case 'ArrowLeft':
      case 'KeyA':
        moveLeft = false;
        break;

      case 'ArrowDown':
      case 'KeyS':
        moveBackward = false;
        break;

      case 'ArrowRight':
      case 'KeyD':
        moveRight = false;
        break;
        
      case 'Space':
        moveUp = false;
        break;
        
      case 'KeyC':
        moveDown = false;
        break;
    }
  };

  document.addEventListener( 'keydown', onKeyDown );
  document.addEventListener( 'keyup', onKeyUp );
  
  // Nastavení načítání modelů
  loader = new GLTFLoader();
  selectedBuilding = document.getElementById('buildingRequest').innerText;

  loader.load(
    `/wwwroot/soubor3D/${selectedBuilding}.gltf`, // URL Zdroje

    function (gltf) {
      scene.add( gltf.scene );
      // gltf.animations; // Array<THREE.AnimationClip>
      // gltf.scene; // THREE.Group
      // gltf.scenes; // Array<THREE.Group>
      // gltf.cameras; // Array<THREE.Camera>
      
      gltf.scene.children.forEach(e => {
        if (e.name.split('-')[1] === 'patro') {
          optionsContainer.appendChild(document.createElement('li'));
          let element = document.createElement('a');
          Object.assign(element, {
            innerHTML: e.name.split('-')[2].replaceAll("_", " "),
            id: e.name.split('-')[0],
            onclick: (e) => {
              selectGroup(e.target.id);
            }
          });
          optionsContainer.lastChild.appendChild(element);
        } else if (e.name.split('-')[1] === 'info') {
          objects.push(e);
        }
      });
      
      optionsContainer.appendChild(document.createElement('li'));
      let element = document.createElement('a');
      Object.assign(element, {
        innerHTML: 'Zobrazit vše',
        onclick: () => {
          selectGroup();
        }
      });
      optionsContainer.lastChild.appendChild(element);
    },
      
    function (xhr) {
      selectedBuilding = ( xhr.loaded / xhr.total * 100 )
      loadingProgress.value = xhr.loaded / xhr.total * 100;
      loadingProgressLabel.innerHTML = `${(Math.round(xhr.loaded / 100000) / 10).toString()} MB / ${(Math.round(xhr.total / 100000) / 10).toString()} MB`
      if (xhr.loaded / xhr.total === 1) {
        loadingContainer.style.display = 'none';
        blocker.style.display = 'block';
      }
    },
      
    function (error) {
      console.log( `Nastala chyba: ${error}` );
      loadingContainer.style.display = 'none';
      errorContainer.style.display = 'flex';
      errorContainer.childNodes[1].innerHTML = `${errorContainer.childNodes[1].innerHTML}<br />${error}`;
    }
  );
  
  // Načtení textur
  textures['pozadi'] = new THREE.TextureLoader().load('../ob/poza.png');
  textures['logo'] = new THREE.TextureLoader().load('../ob/logo.png');

  // Listeners
  window.addEventListener( 'resize', canvasResize );
  renderContainer.addEventListener('mouseover', () => { hasFocus = true });
  renderContainer.addEventListener('mouseleave', () => { hasFocus = false });
  
  console.log('Initialization done 😎');
}

// Už vím, co to dělá
function dotek() {
  raycaster.set(controls.getObject().position, controls.getDirection(new THREE.Vector3()));
  const intersect = raycaster.intersectObjects(objects, true).length > 0 ? raycaster.intersectObjects(objects, true)[0] : null;

  // Ukáže vytvořený ray (dobrý pro debug)
  //scene.add( new THREE.ArrowHelper(controls.getDirection(new THREE.Vector3()), controls.getObject().position, 50, 0xFFFFFF));
  
  if (intersect != null && intersect.object.name !== "" && intersect.object.name !== undefined) {
    if (intersect.object.name.split('-')[1] === "info" && intersect.object.visible) {
      //Vyplnění info panelu v html
      $.ajax(`/Navigace/UcebnaData/${intersect.object.name.split('-')[2].replaceAll("_", " ")}`)
        .fail(function() {
          roomInfo.innerHTML = `<div><b>${intersect.object.name.split('-')[2].replaceAll('_', ' ')}</b></div>`;
          roomInfo.innerHTML += `<div>Data nejsou k dispozici</div>`;
          roomContainer.style.display = 'block';
        })
        .done(function(data) {
          if (data === undefined) {
            roomInfo.innerHTML = `<h3><b><i>${intersect.object.name.split('-')[2].replaceAll('_', ' ')}</i></b></h3>`;
            roomInfo.innerHTML += `<div>Data nejsou k dispozici</div>`;
          } else {
           roomInfo.innerHTML = `<h3><b>${data.druh === 'Učebna' ? data.druh + ' ' : ''}${data.nazev.replaceAll('_', ' ')}</b></h3>`;
           roomInfo.innerHTML += data.druh !== 'Učebna' ? `<div><b>Druh místnosti:</b> ${data.druh}</div>` : '';
           roomInfo.innerHTML += data.poddruh !== '' ? `<div><b>Dodatečné informace:</b> ${data.poddruh}</div>` : '';
           roomInfo.innerHTML += `<div><b><a class="button" href="/Navigace/UcebnaDetail/${data.id}">Více informací</a></b></div>`
          }
         roomContainer.style.display = 'block';
        }
      );
    }
  }
}

// Přizpůsobení oknu
function canvasResize() {
  const height = $('#renderContainer').innerHeight();
  camera.aspect = renderContainer.clientWidth / height;
  camera.updateProjectionMatrix();
  
  renderer.setSize(renderContainer.clientWidth, height);
}

// Animate loop
function animate() {
  requestAnimationFrame(animate);
  
  const time = performance.now();

  if (controls.isLocked === true) {
    const delta = ( time - prevTime ) / 1000;
    
    velocity.x -= velocity.x * 10.0 * delta;
    velocity.y -= velocity.y * 10.0 * delta;
    velocity.z -= velocity.z * 10.0 * delta;

    direction.x = Number(moveRight) - Number(moveLeft);
    direction.y = Number(moveDown) - Number(moveUp);
    direction.z = Number(moveForward) - Number(moveBackward);
    direction.normalize();

    if (moveLeft || moveRight) velocity.x -= direction.x * 600.0 * delta;
    if (moveUp || moveDown) velocity.y -= direction.y * 600.0 * delta;
    if (moveForward || moveBackward) velocity.z -= direction.z * 600.0 * delta;
    
    controls.moveRight(- velocity.x * delta);
    controls.moveForward(- velocity.z * delta);
    controls.getObject().position.y += velocity.y * delta;
  }

  prevTime = time;

  renderer.render( scene, camera );
}

// Přepnutí zobrazení části modelu
function selectGroup(id = null) {
  scene.children[scene.children.length - 1].children.forEach(e => {
    e.visible = id === null ? true : !!e.name.includes(id);
  })
}
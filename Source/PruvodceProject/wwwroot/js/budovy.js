import * as THREE from 'three';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { PointerLockControls } from 'three/addons/controls/PointerLockControls.js';

THREE.Cache.enabled = true;

let renderContainer, blocker, instructions, loadingContainer, loadingProgress, roomContainer, roomInfo, errorContainer,
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

  obd(24,10,20,textures['logo'],"", "infoT1");
  obd(8,10,20,textures['logo'],"", "infoT2");
  obd(16,10,20,textures['logo'],"", "infoT5");
  obd(20,10,20,textures['logo'],"", "infoT6");
  obd(0,10,20,textures['logo'],"", "infoT15");
});

// Inicializace
function init() {
  // HTML Elementy
  renderContainer = document.getElementById('renderer');
  blocker = document.getElementById( 'blocker' );
  instructions = document.getElementById( 'instructions' );
  loadingContainer = document.getElementById('loadingContainer');
  loadingProgress = document.getElementById('loadingProgress');
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
    renderContainer.addEventListener('click', dotek);
  });

  controls.addEventListener('unlock', () => {
    blocker.style.display = 'block';
    roomContainer.style.display = 'none';
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
      gltf.animations; // Array<THREE.AnimationClip>
      gltf.scene; // THREE.Group
      gltf.scenes; // Array<THREE.Group>
      gltf.cameras; // Array<THREE.Camera>
      
      scene.children[scene.children.length - 1].children.forEach(e => {
        optionsContainer.appendChild(document.createElement('li'));
        let element = document.createElement('a');
        Object.assign(element, {
          innerHTML: e.name.replace('patro-', ''),
          id: e.name,
          onclick: (e) => {
            selectGroup(e.target.id)
          }
        });
        optionsContainer.lastChild.appendChild(element);
      });
      
      optionsContainer.appendChild(document.createElement('li'));
      let element = document.createElement('a');
      Object.assign(element, {
        innerHTML: 'Zobrazit vše',
        onclick: () => {
          selectGroup()
        }
      });
      optionsContainer.lastChild.appendChild(element);
    },
      
    function (xhr) {
      selectedBuilding = ( xhr.loaded / xhr.total * 100 )
      loadingProgress.value = xhr.loaded / xhr.total * 100;
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
}

// Pro objekty, bez potřeby psaní scena.add(....)
// Tyto objekty nemají žádnou interakci s myší (tedy teď mají ale potom nebudou)
function obd(x, y, z, obrazek, barva, jmeno) {
  let ct = new THREE.Mesh(
      new THREE.SphereGeometry(3, 32, 32),
      new THREE.MeshStandardMaterial({ map: obrazek, color:barva })
  );
  ct.position.x = x;
  ct.position.y = y;
  ct.position.z = z;
  ct.name = jmeno
  scene.add(ct);
  objects.push(ct);
}

// Už vím, co to dělá
function dotek() {
  raycaster.set(controls.getObject().position, controls.getDirection(new THREE.Vector3()));
  const intersects = raycaster.intersectObjects(objects, true);

  // Ukáže vytvořený ray (dobrý pro debug)
  //scene.add( new THREE.ArrowHelper(controls.getDirection(new THREE.Vector3()), controls.getObject().position, 50, 0xFFFFFF));
  
  if (intersects.length > 0 && intersects[0].object.name !== "" && intersects[0].object.name !== undefined){
    let variace = intersects[0].object.name.substring(0,4);
    if (variace === "info") {
      //Požadavek informací ze serveru
      //InfoOMistnosti(intersects[0].object.name)
  
      //Vyplnění info panelu v html
      roomContainer.style.display = 'block';
      roomInfo.innerHTML = intersects[0].object.name.substring(4);
    }
  }
}

// Přizpůsobení oknu
function canvasResize() {
  const height = renderContainer.parentElement.parentElement.clientHeight - 4.3 * Number(window.getComputedStyle(renderContainer).fontSize.replace('px', ''));
  camera.aspect = renderContainer.clientWidth / height;
  camera.updateProjectionMatrix();
  
  blocker.style.height = height + "px";
  roomContainer.style.height = height + "px";
  loadingContainer.style.height = height + "px";
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
function selectGroup(name = null) {
  if (name === null) {
    scene.children[scene.children.length - 1].children.forEach(e => {
      e.visible = true;
    })
  } else {
    scene.children[scene.children.length - 1].children.forEach(e => {
      e.visible = false;
    })
    
    scene.getObjectByName(name).visible = true;
  }
}
//import './style.css';
import * as THREE from 'three';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { FlyControls } from 'three/addons/controls/FlyControls.js';


window.addEventListener("load", (event) => {
  /////Pro objekty, bez potřeby psaní scena.add(....)
  /////Tyto objetky nemají žádnou interakci s myší (tedy teď mají ale potom nebudou)
  function obd(x,y,z,obrazek,barva,jmeno){
    var ct = new THREE.Mesh(
      new THREE.SphereGeometry(3,32,32), 
      new THREE.MeshStandardMaterial({map: obrazek, color:barva}));
    ct.position.x = x;
    ct.position.y = y;
    ct.position.z = z;
    ct.name = jmeno
    scena.add(ct);
  }
  //#region Základ/nestavení
  const scena = new THREE.Scene();
  const kamera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
  const renderer = new THREE.WebGLRenderer({
    canvas: document.querySelector('#bg'),
  });


  /////Tohle mapování myši je pro interakci objektů
  const raycaster = new THREE.Raycaster();
  const mouse = new THREE.Vector2();

  function mysPohyb(e){
    mouse.x = (e.clientX / window.innerWidth)*2 -1;
    mouse.y = -(e.clientY / window.innerHeight) * 2 +1;
  }

  var variace = "";

  function dotek(){
    raycaster.setFromCamera(mouse, kamera);
    const intersects = raycaster.intersectObjects(scena.children);
    if(intersects[0].object.name != "" && intersects[0].object.name != undefined){
      
      variace = intersects[0].object.name.substring(0,4);
      intersects[0].object.name = intersects[0].object.name.substring(4);

      if(variace == "info"){
        //Požadavek informací ze serveru
        InfoOMistnosti(intersects[0].object.name)
        
        //Vyplnění info panelu v html
        document.getElementById("panelMistnostiPrekryvaPlatno").style.display = 'block';
        document.getElementById("popisMistnosti").innerHTML = intersects[0].object.name;
      }
    }
  }

  const procentaNacitani = document.getElementById('procentaNacitani');
  const procentaNacitaniContainer = document.querySelector('.procentaNacitaniContainer');

  const loader = new GLTFLoader();
  const managerNacitani = new THREE.LoadingManager();
  
  managerNacitani.onProgress = function(url, nacteno, total){
    procentaNacitani.value = (nacteno / total) * 100;
  }
  managerNacitani.onLoad = function() {
    procentaNacitaniContainer.style.display = 'none';
  }

  var l = 0

  //Proměnná pro univerzální načítání jakéholiv .gltf souboru
  const vybranamistnost = '/wwwroot/soubor3D/skolni101.gltf'
  //const vybranamistnost = '/wwwroot/soubor3D/horska618.gltf'
  //const vybranamistnost = '/wwwroot/soubor3D/horska59.gltf'
  //const vybranamistnost = '/wwwroot/soubor3D/mladebuky.gltf'

  //Načtení modelu
  loader.load(
    // resource URL
    vybranamistnost,

    function ( gltf ) {
      scena.add( gltf.scene );
      gltf.animations; // Array<THREE.AnimationClip>
      gltf.scene; // THREE.Group
      gltf.scenes; // Array<THREE.Group>
      gltf.cameras; // Array<THREE.Camera>
    },
    function ( xhr ) {
      l = ( xhr.loaded / xhr.total * 100 )
      console.log( ( xhr.loaded / xhr.total * 100 ) + '% loaded' );
    },
    function ( error ) {
      console.log( 'Nastala chyba' );
    }
  );

  const logo = new THREE.TextureLoader().load('../ob/logo.png');
  const pozadi = new THREE.TextureLoader().load('../ob/poza.png');

  renderer.setPixelRatio(window.devicePixelRatio);
  /////FULLSCREEN
  renderer.setSize(window.innerWidth, window.innerHeight);
  /////Ať nejsem na začítaku ve středu
  kamera.position.setZ(40);
  kamera.position.setY(4);
  /////renderer = DRAW (něco jako ve hře)
  renderer.render(scena, kamera);
  //#endregion

  //#region  Světlo
  /////Světlo pro celou mapu (nejsou stíny)
  const svetlo = new THREE.AmbientLight(0xffffff);
  //#endregion

  /////Tohle mapování myši je pro pohyb po mapě
  //const pohyb = new OrbitControls(kamera, renderer.domElement);
  const pohyb = new FlyControls(kamera, renderer.domElement)
  pohyb.dragToLook = true;
  pohyb.rollSpeed = 0.05;
  pohyb.movementSpeed = 0.5;


  window.addEventListener('click', dotek);
  //window.addEventListener('click', stop, true);
  window.addEventListener('mousemove', mysPohyb, false);

  obd(20,10,20,logo,"", "infoT1");
  obd(10,10,20,logo,"", "infoT2");
  obd(20,10,20,logo,"", "infoT5");
  obd(20,10,20,logo,"", "infoT6");
  obd(18,10,20,logo,"","skskskkskskss");
  obd(8,10,20,logo,"", "infoT15");

  scena.add(svetlo);
  scena.background = pozadi;
  //#endregion

  function animate(){
    requestAnimationFrame(animate);
    pohyb.update(0.5);
    renderer.render(scena, kamera);
  }

  animate();
});
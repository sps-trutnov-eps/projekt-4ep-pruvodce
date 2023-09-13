//import './style.css';
import * as THREE from 'three';
import { OrbitControls } from 'three/addons/controls/OrbitControls.js';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';

//Pouze 4 patra, není potřeba dělat extra fuce
$(document).ready(function(){
  $("#priz").click(function(){
    vyberPodlahy(0)
  });
  $("#1pat").click(function(){
    vyberPodlahy(1)
  });
  $("#2pat").click(function(){
    vyberPodlahy(2)
  });
  $("#3pat").click(function(){
    vyberPodlahy(3)
  });
  $("#vsec").click(function(){
    vyberPodlahy(4)
  });
});

const loader = new GLTFLoader();
// const skolniLoader = new SkolniLoader()
// skolniLoader.setDecoderPath('')

loader.load(
	// resource URL
	'../ob/skolni101.gltf',
	// called when the resource is loaded
	function ( gltf ) {
		scena.add( gltf.scene );
		gltf.animations; // Array<THREE.AnimationClip>
		gltf.scene; // THREE.Group
		gltf.scenes; // Array<THREE.Group>
		gltf.cameras; // Array<THREE.Camera>

	},
	function ( xhr ) {
		console.log( ( xhr.loaded / xhr.total * 100 ) + '% loaded' );
	},
	function ( error ) {
		console.log( 'Nastala chyba' );
	}
);

/////Pro objekty, bez potřeby psaní scena.add(....)
/////Tyto objetky nemají žádnou interakci s myší (tedy teď mají ale potom nebudou)
function obd(otoceni,w,h,d,x,y,z,obrazek,barva,skupina,jmeno){
  // if (typ === 0){
    var ct = new THREE.Mesh(new THREE.BoxGeometry(w,h,d), new THREE.MeshBasicMaterial({map: obrazek, color:barva}));
  // } 
  // else if (typ === 1){
  //   var ct = new THREE.Mesh(
  //     new THREE.SphereGeometry(w,h,d), 
  //     new THREE.MeshStandardMaterial({map: obrazek, color:barva}));
  // }
  if(otoceni === true){
    ct.position.z = z;
    ct.position.x = x + d / 2;
    ct.position.y = y + h / 2;
    ct.rotateY(1.57);
  }else{
    ct.position.x = x + w / 2;
    ct.position.y = y + h / 2;
    ct.position.z = z + d / 2;
  }

  ct.position.x -= 300;
  ct.position.z -= 250;
  ct.position.y -= 100;
  ct.name = jmeno
  if(skupina !== undefined){
    skupina.add(ct)
  }else{
    scena.add(ct);
  }
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
  mouse.x = (e.clientX / window.innerWidth)*2-1;
  mouse.y = -(e.clientY / window.innerHeight)*2+1;
}

function dotek(){
  raycaster.setFromCamera(mouse, kamera);
  const intersects = raycaster.intersectObjects(scena.children);
  if(intersects[0].object.name != "" && intersects[0].object.name != undefined){
    const novyMaterial = intersects[0].object.material.clone();
    novyMaterial.transparent = true;
    novyMaterial.opacity = 0.5;
    intersects[0].object.material = novyMaterial;
  }
}

window.addEventListener('click', dotek);
window.addEventListener('mousemove', mysPohyb, false);

const pozadi = new THREE.TextureLoader().load('../ob/poza.png');

renderer.setPixelRatio(window.devicePixelRatio);
/////FULLSCREEN
renderer.setSize(window.innerWidth, window.innerHeight);
/////Ať nejsem na začítaku ve středu
kamera.position.setZ(240);
kamera.position.setY(20);
/////renderer = DRAW (něco jako ve hře)
renderer.render(scena, kamera);
//#endregion

//#region  Světlo
/////Světlo pro celou mapu (nejsou stíny)
const svetlo = new THREE.AmbientLight(0xffffff);
//#endregion

/////Tohle mapování myši je pro pohyb po mapě
const pohyb = new OrbitControls(kamera, renderer.domElement);

const prizemi = new THREE.Group()
const prvniPatro = new THREE.Group()
const druhePatro = new THREE.Group()
const tretiPatro = new THREE.Group()

function vyberPodlahy(vybr){
  switch(vybr){
    case 0:
      prizemi.position.y = 0
      prizemi.visible = true
      prvniPatro.visible = false
      druhePatro.visible = false
      tretiPatro.visible = false
    break;
    case 1:
      prizemi.visible = false
      prvniPatro.visible = true
      druhePatro.visible = false
      tretiPatro.visible = false
    break;
    case 2:
      prizemi.visible = false
      prvniPatro.visible = false
      druhePatro.visible = true
      tretiPatro.visible = false
    break;
    case 3:
      prizemi.visible = false
      prvniPatro.visible = false
      druhePatro.visible = false
      tretiPatro.visible = true
    break;
    case 4:
      prizemi.visible = true
      prvniPatro.visible = true
      druhePatro.visible = true
      tretiPatro.visible = true
    break;
}}

//Výška zdi
var vZ = 60
//Síla zdi
var sZ = 10

//Délka podlahy X
var dP = 521
//Délka podlahy Z
var zP = 333
//Síla podlahy
var sP = 2

//podlaha
obd(false,dP,sP,zP ,  0,-2,-2,              0,0xDDBE90, prizemi);
obd(false,176+sZ,sP,  141, 162+sZ,-1.9,-31, 0,0xDDBE00, prizemi);

obd(false,dP,sP,zP ,  0,-2+sP+vZ,-2, 0,     0xDDBE90, prvniPatro);
obd(false,176+sZ,sP,  141, 162+sZ,-1.9+sP+vZ,-31, 0,0xDDBE00, prvniPatro);


//podlaha (pouze pro 3.patro)



//Venkovní zdi


//#endregion
///////0xED1C24, 0xBDA299, 0xFEFFD7, 0xDDBE90, 0x5B4E45
scena.add(prizemi)
scena.add(prvniPatro)
scena.add(druhePatro)
scena.add(tretiPatro)
scena.add(svetlo);
scena.background = pozadi;

function animate(){
  requestAnimationFrame(animate);
  // torus.rotation.x += 0.02;

  pohyb.update();
  
  renderer.render(scena, kamera);
}

animate();

//import './style.css';
import * as THREE from 'three';
import { GLTFLoader } from 'three/addons/loaders/GLTFLoader.js';
import { FlyControls } from 'three/addons/controls/FlyControls.js';



/////Pro objekty, bez potřeby psaní scena.add(....)
/////Tyto objetky nemají žádnou interakci s myší (tedy teď mají ale potom nebudou)
function obd(w,h,d,x,y,z,typ,obrazek,barva,jmeno){
  if (typ === 0){
    var ct = new THREE.Mesh(new THREE.BoxGeometry(w,h,d), new THREE.MeshBasicMaterial({map: obrazek, color:barva}));
  } 
  else if (typ === 1){
    var ct = new THREE.Mesh(
      new THREE.SphereGeometry(w,h,d), 
      new THREE.MeshStandardMaterial({map: obrazek, color:barva}));
  }
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

function dotek(){
  raycaster.setFromCamera(mouse, kamera);
  const intersects = raycaster.intersectObjects(scena.children);
  if(intersects[0].object.name != "" && intersects[0].object.name != undefined){
    document.getElementById("Mistnostvyber").style.display = 'block';
    document.getElementById("ins").innerHTML = intersects[0].object.name;

    // const novyMaterial = intersects[0].object.material.clone();
    // novyMaterial.transparent = true;
    // novyMaterial.opacity = 0.5;
    // intersects[0].object.material = novyMaterial;

    
  }
}

const loader = new GLTFLoader();

//Načtení modelu
loader.load(
	// resource URL
	'/wwwroot/soubor3D/skolni101.gltf',

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


const KrychleLogo = new THREE.TextureLoader().load('../ob/logosss.png');
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

// function stop(){
//   pohyb.dragToLook = false;
// }

//Nad dveřmi
const kruh = new THREE.TorusGeometry(11, 3, 16, 100, 3.2);
const material = new THREE.MeshStandardMaterial({color: 0xFEFFD7});
const torus = new THREE.Mesh(kruh, material);  
torus.position.z = -11
torus.position.y = 18


obd(3,3,3,20,0,0,0,KrychleLogo);
obd(3,3,3,30,0,0,0,logo);

obd(3,32,32,20,10,20,1,logo,"", "inteSk1iuvdskjbhjnknbfdkjnbbfdnkjfdhkjbfd");
obd(3,32,32,18,10,20,1,logo,"","integjbfdnkjnbfdnbfdkjbfdnkjnbfdkjnbfdkjbfdkjgsijgfdihgfdkjgfdshkjgdshkjghkjgfdshkjgdshkjgfdshgfdshgkjshkjgewshhhgeshnbfdjnbjfdnkjbfdnkjSk2");
obd(3,32,32,8,10,20,1,logo,"", "inteSijhgkfdjnblkfdk3");


scena.add(svetlo);
scena.background = pozadi;
//#endregion

function animate(){
  requestAnimationFrame(animate);
  // torus.rotation.x += 0.02;

  pohyb.update(0.5);
  
  renderer.render(scena, kamera);
}

animate();

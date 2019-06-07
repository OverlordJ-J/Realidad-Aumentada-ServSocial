PShape sun;
PImage suntex;
PImage test;

PShape planet1;
PImage surftex1;
PShape orbit1;

PShape planet2;
PImage surftex2;
PShape orbit2;

PShape planet3;
PImage surftex3;
PShape orbit3;

PShape planet4;
PImage surftex4;
PShape orbit4;

PShape planet5;
PImage surftex5;
PShape orbit5;

PShape planet6;
PImage surftex6;
PShape orbit6;

PShape planet7;
PImage surftex7;
PShape orbit7;

PShape planet8;
PImage surftex8;
PShape orbit8;

PImage starfield;

float ry, ryT;

void setup(){
  size(1024, 768, P3D);
  smooth();
  
  test = loadImage("test.jpg");
  starfield = loadImage("2k_stars.jpg");
  suntex = loadImage("2k_sun.jpg");
  surftex1 = loadImage("2k_mercury.jpg");
  surftex2 = loadImage("2k_venus_surface.jpg");
  surftex3 = loadImage("8k_earth.jpg");
  surftex4 = loadImage("2k_mars.jpg");
  surftex5 = loadImage("2k_jupiter.jpg");
  surftex6 = loadImage("2k_saturn.jpg");
  surftex7 = loadImage("2k_uranus.jpg");
  surftex8 = loadImage("2k_neptune.jpg");
  noStroke();
  fill(255);
  sphereDetail(40);
    
  sun = createShape(SPHERE, 150);
  sun.setTexture(suntex);  

  planet1 = createShape(SPHERE, 20);
  planet2 = createShape(SPHERE, 50);
  planet3 = createShape(SPHERE, 52);
  planet1.setTexture(surftex1);  
  planet2.setTexture(surftex2);
  //planet2.setTexture(test);
  planet3.setTexture(surftex3);
  //planet4.setTexture(surftex4);
  //planet5.setTexture(surftex5);
  //planet6.setTexture(surftex6);
  //planet7.setTexture(surftex7);
  //planet8.setTexture(surftex8);
  noFill();
  stroke(255,255,255);
  //scale(-1,-1,-1);
}

void draw(){
  
  background(0);
  
  hint(DISABLE_DEPTH_MASK);
  image(starfield, 0, 0, width, height);
  hint(ENABLE_DEPTH_MASK);

  //pointLight(255,  255,  255,  0,  0,  0);  
  translate(width/2,height/2, 0);
  rotateY(ry);
  
  pushMatrix();
  scale(1,-1,-1);
  //rotateY(PI * frameCount / 500);
  noLights();
  shape(sun);
  popMatrix();  
  
  pushMatrix();
  translate(200,0,150);
  pointLight(255, 255,  255, -400, 0, -150);
  rotateY(ry);
  rotateZ(PI);
  rotateX(PI);
  shape(planet1);
  //shape(orbit1);
  popMatrix();
  
  pushMatrix();
  translate(300,0,100);
  //pointLight(255, 255,  255, -400, 0, -150);
  rotateY(ry);
  rotateZ(PI);
  rotateX(PI);
  shape(planet2);
  //shape(orbit1);
  popMatrix();
  
  pushMatrix();
  translate(450,0,100);
  rotateY(ry);
  rotateZ(PI);
  rotateX(PI);
  shape(planet3);
  //shape(orbit1);
  popMatrix();
  
  ry += 0.02;
}
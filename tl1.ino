
const byte pin_fwd = 7;
const byte pin_bwd = 8;
const byte pin_pwm = 6;
const byte pin_b = 2;
const byte pin_a = 3;
int encoder = 0;
int m_direction = 0;
int sv_speed = 100;
int i=0;
double pv_speed = 0;
double set_speed = 0;
double e_speed = 0;
double e_speed_pre = 0;
double e_speed_sum = 0; 
double pwm_pulse = 0;
double kp = 0.07;
double ki = 0.05;
double kd = 0.03;
char myChar;
boolean stringComplete = false; 
boolean motor_start = false;
int timer1_counter;



unsigned long moment = millis();

//2 serial. Serial
void setup() 
{
  pinMode(pin_a,INPUT_PULLUP);
  pinMode(pin_b,INPUT_PULLUP);
  pinMode(pin_fwd,OUTPUT);
  pinMode(pin_bwd,OUTPUT);
  pinMode(pin_pwm,OUTPUT);
  attachInterrupt(digitalPinToInterrupt(pin_a), detect_a, RISING);
  
  Serial.begin(9600);

  Serial.setTimeout(100);
  
  noInterrupts();
  TCCR1A = 0;
  TCCR1B = 0;
  timer1_counter = 59286;
  TCNT1 = timer1_counter;
  TCCR1B |= (1 << CS12);
  TIMSK1 |= (1 << TOIE1);
  interrupts();

  analogWrite(pin_pwm,0);   //stop motor
  digitalWrite(pin_fwd,0);  //stop motor
  digitalWrite(pin_bwd,0);  //stop motor
}
void loop() 
{
  if (motor_start){
    
    if (millis() - moment >= 100){ //khong duoc update du lieu lien tuc trong ham loop ma khong dieu khien thoi gian
      Serial.println("speed"+String(pv_speed));
      moment = millis(); 
    }
  }

  if(!Serial.available()){
    //khi serial chua doc het du lieu
    return;
  }

  String serialData = Serial.readString(); //doc ca chuoi vao bien serialData

  if (serialData.length() < 5){
    Serial.println("Data length is not enough");
    return;
  }

 

  
  if (serialData.substring(0,8) == "vs_start")
  {
    digitalWrite(pin_fwd,1);      //run motor run forward
    digitalWrite(pin_bwd,0);
    motor_start = true;

    Serial.println("motor started");
  }
  if (serialData.substring(0,7) == "vs_stop")
  {
    digitalWrite(pin_fwd,0);
    digitalWrite(pin_bwd,0);      //stop motor
    motor_start = false;
  }
  if (serialData.substring(0,12) == "vs_set_speed")
  {
    set_speed = serialData.substring(12,serialData.length()).toFloat();  //get string after set_speed
    Serial.println(set_speed);
  }
  if (serialData.substring(0,5) == "vs_kp")
  {
    kp = serialData.substring(5,serialData.length()).toFloat(); //get string after vs_kp
  }
  if (serialData.substring(0,5) == "vs_ki")
  {
    ki = serialData.substring(5,serialData.length()).toFloat(); //get string after vs_ki
  }
  if (serialData.substring(0,5) == "vs_kd")
  {
    kd = serialData.substring(5,serialData.length()).toFloat(); //get string after vs_kd
  }

} 
void detect_a() 
{
  encoder+=1; //increasing encoder at new pulse
  m_direction = digitalRead(pin_b); //read direction of motor
}
ISR(TIMER1_OVF_vect) //nhung thu nhanh nhat se duoc them vao Interrupt, cham nhat thi bo qua no
{
  TCNT1 = timer1_counter;   // set timer
  pv_speed = 60.0*(encoder/200.0)/0.1;  //calculate motor speed, unit is rpm
  encoder=0;
  //print out speed
  //Serial.println(pv_speed);


  //PID program
  if (motor_start){
    e_speed = set_speed - pv_speed;
    pwm_pulse = e_speed*kp + e_speed_sum*ki + (e_speed - e_speed_pre)*kd;
    e_speed_pre = e_speed;  //save last (previous) error
    e_speed_sum += e_speed; //sum of error
    if (e_speed_sum >4000) e_speed_sum = 4000;
    if (e_speed_sum <-4000) e_speed_sum = -4000;
  }
  else{
    e_speed = 0;
    e_speed_pre = 0;
    e_speed_sum = 0;
    pwm_pulse = 0;
  }
  

  //update new speed
  if (pwm_pulse <255 & pwm_pulse >0){
    analogWrite(pin_pwm,pwm_pulse);  //set motor speed  
  }
  else{
    if (pwm_pulse>255){
      analogWrite(pin_pwm,255);
    }
    else{
      analogWrite(pin_pwm,0);
    }
  }
  
}

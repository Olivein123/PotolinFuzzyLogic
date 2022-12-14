using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotFuzzy;
namespace AliacFuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection speed,angle,throttle;
        LinguisticVariable myspeed, myangle, mythrottle;
        FuzzyRuleCollection myrules;
        

        public Form1()
        {
            InitializeComponent();
        }

    
        public void setMembers()
        {

            speed = new MembershipFunctionCollection();
            speed.Add(new MembershipFunction("LOW",0.0,0.0,45.0,50.0));
            speed.Add(new MembershipFunction("OK", 45.0, 50.0, 50.0, 55.0));
            speed.Add(new MembershipFunction("HIGH", 50.0, 55.0, 100.0, 100.0));
            myspeed = new LinguisticVariable("SPEED", speed);


            angle = new MembershipFunctionCollection();
            angle.Add(new MembershipFunction("DOWN", -10.0, -10.0, -5.0, 0.0));
            angle.Add(new MembershipFunction("LEVEL", -5.0, 1.0, 1.0, 5.0));
            angle.Add(new MembershipFunction("UP", 0.0, 5.0, 10.0, 10.0));
            myangle = new LinguisticVariable("ANGLE", angle);

            throttle = new MembershipFunctionCollection();
            throttle.Add(new MembershipFunction("LOW",0.0,0.0,2.0,4.0));
            throttle.Add(new MembershipFunction("LM", 2.0, 4.0, 4.0, 6.0));
            throttle.Add(new MembershipFunction("MED", 4.0, 6.0, 6.0, 8.0));
            throttle.Add(new MembershipFunction("HM", 6.0, 8.0, 8.0, 10.0));
            throttle.Add(new MembershipFunction("HIGH", 8.0, 10.0, 10.0, 10.0));
            mythrottle = new LinguisticVariable("THROTTLE", throttle);

            
        
        }

        public void setRules()
        {
          myrules = new FuzzyRuleCollection();
          myrules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (ANGLE IS UP) THEN THROTTLE IS LM"));
          myrules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (ANGLE IS LEVEL) THEN THROTTLE IS LM"));
          myrules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (ANGLE IS DOWN) THEN THROTTLE IS LOW"));
          myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (ANGLE IS UP) THEN THROTTLE IS HM"));
          myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (ANGLE IS LEVEL) THEN THROTTLE IS MED"));
          myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (ANGLE IS DOWN) THEN THROTTLE IS LM"));
          myrules.Add(new FuzzyRule("IF (SPEED IS LOW) AND (ANGLE IS UP) THEN THROTTLE IS HIGH"));
          myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (ANGLE IS LEVEL) THEN THROTTLE IS HM"));
          myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (ANGLE IS DOWN) THEN THROTTLE IS HM"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(myspeed);
            fe.LinguisticVariableCollection.Add(myangle);
            fe.LinguisticVariableCollection.Add(mythrottle);
            fe.FuzzyRuleCollection = myrules;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void defuziffyToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMembers();
            setRules();
            //setFuzzyEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myspeed.InputValue=(Convert.ToDouble(textBox1.Text));
            myspeed.Fuzzify("OK");
            
            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            myangle.InputValue = (Convert.ToDouble(textBox2.Text));
            myangle.Fuzzify("LEVEL");
            
        }

        public void fuziffyvalues()
        {
            myspeed.InputValue = (Convert.ToDouble(textBox1.Text));
            myspeed.Fuzzify("LOW");
            myangle.InputValue = (Convert.ToDouble(textBox2.Text));
            myangle.Fuzzify("DOWN");
        
        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "THROTTLE";
            textBox3.Text = "" + fe.Defuzzify();
        }

        public void computenewspeed()
        {

            double oldspeed = Convert.ToDouble(textBox1.Text);
            double oldthrottle = Convert.ToDouble(textBox3.Text);
            double oldangle = Convert.ToDouble(textBox2.Text);
            double newspeed = ((1 - 0.1) * (oldspeed)) + (oldthrottle - (0.1 * oldangle));
            textBox1.Text = "" + newspeed;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            fe.Consequent = "THROTTLE";
            textBox3.Text = "" + fe.Defuzzify();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            computenewspeed();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fuziffyvalues();
            defuzzy();
            computenewspeed();
        }

       
    }
}

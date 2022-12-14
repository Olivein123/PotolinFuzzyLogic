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
        MembershipFunctionCollection hp,numPlayers,bossAction;
        LinguisticVariable myHp, myNumPlayers, myBossAction;
        FuzzyRuleCollection myrules;
        

        public Form1()
        {
            InitializeComponent();
        }

    
        public void setMembers()
        {
            hp = new MembershipFunctionCollection();
            hp.Add(new MembershipFunction("VERYLOW",0.0,0.0,5.0,15.0));
            hp.Add(new MembershipFunction("LOW", 10.0, 20.0, 20.0, 25.0));
            hp.Add(new MembershipFunction("HALF", 25.0, 50.0, 50.0, 50.0));
            hp.Add(new MembershipFunction("HIGH", 50.0, 75.0, 75.0, 80.0));
            hp.Add(new MembershipFunction("FULL", 75.0, 100.0, 100.0, 100.0));
            myHp = new LinguisticVariable("HEALTH", hp);

            numPlayers = new MembershipFunctionCollection();
            numPlayers.Add(new MembershipFunction("NONE", 0.0, 0.0, 0.0, 0.0));
            numPlayers.Add(new MembershipFunction("SMALLGROUP", 1.0, 2.0, 2.0, 2.0));
            numPlayers.Add(new MembershipFunction("GROUP", 2.0, 3.0, 3.0, 4.0));
            numPlayers.Add(new MembershipFunction("PLATOON", 4.0, 5.0, 5.0, 6.0));
            numPlayers.Add(new MembershipFunction("BATTALION", 6.0, 7.0, 7.0, 8.0));
            numPlayers.Add(new MembershipFunction("GUILD", 8.0, 9.0, 9.0, 10.0));
            myNumPlayers = new LinguisticVariable("ATTACKINGPLAYERS", numPlayers);

            bossAction = new MembershipFunctionCollection();
            bossAction.Add(new MembershipFunction("DONOTHING",0.0,0.0,2.0,4.0));
            bossAction.Add(new MembershipFunction("HEAL", 2.0, 4.0, 4.0, 6.0));
            bossAction.Add(new MembershipFunction("ATTACK", 4.0, 6.0, 6.0, 8.0));
            bossAction.Add(new MembershipFunction("INVULNERABILITY", 6.0, 8.0, 8.0, 10.0));
            bossAction.Add(new MembershipFunction("ENRAGE", 8.0, 10.0, 10.0, 10.0));
            myBossAction = new LinguisticVariable("ACTION", bossAction);
        }

        public void setRules()
        {
          myrules = new FuzzyRuleCollection();
          myrules.Add(new FuzzyRule("IF (HEALTH IS VERYLOW) AND (ATTACKINGPLAYERS IS NONE) THEN ACTION IS HEAL"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS VERYLOW) AND (ATTACKINGPLAYERS IS SMALLGROUP) THEN ACTION IS INVULNERABILITY"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS VERYLOW) AND (ATTACKINGPLAYERS IS GUILD) THEN ACTION IS ENRAGE"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS LOW) AND (ATTACKINGPLAYERS IS NONE) THEN ACTION IS HEAL"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS LOW) AND (ATTACKINGPLAYERS IS SMALLGROUP) THEN ACTION IS ATTACK"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS LOW) AND (ATTACKINGPLAYERS IS GUILD) THEN ACTION IS INVULNERABILITY"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS HALF) AND (ATTACKINGPLAYERS IS NONE) THEN ACTION IS HEAL"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS HALF) AND (ATTACKINGPLAYERS IS PLATOON) THEN ACTION IS ATTACK"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS HALF) AND (ATTACKINGPLAYERS IS GUILD) THEN ACTION IS INVULNERABILITY"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS HIGH) AND (ATTACKINGPLAYERS IS NONE) THEN ACTION IS HEAL"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS HIGH) AND (ATTACKINGPLAYERS IS BATTALION) THEN ACTION IS ATTACK"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS HIGH) AND (ATTACKINGPLAYERS IS GUILD) THEN ACTION IS INVULNERABILITY"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS FULL) AND (ATTACKINGPLAYERS IS NONE) THEN ACTION IS DONOTHING"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS FULL) AND (ATTACKINGPLAYERS IS BATTALION) THEN ACTION IS ENRAGE"));
          myrules.Add(new FuzzyRule("IF (HEALTH IS FULL) AND (ATTACKINGPLAYERS IS GUILD) THEN ACTION IS ATTACK"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(myHp);
            fe.LinguisticVariableCollection.Add(myNumPlayers);
            fe.LinguisticVariableCollection.Add(myBossAction);
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
            myHp.InputValue=(Convert.ToDouble(textBox1.Text));
            myHp.Fuzzify("LOW");  
        }
        private void button2_Click(object sender, EventArgs e)
        {
            myNumPlayers.InputValue = (Convert.ToDouble(textBox2.Text));
            myNumPlayers.Fuzzify("SMALLGROUP");
            
        }

        public void fuziffyvalues()
        {
            myHp.InputValue = (Convert.ToDouble(textBox1.Text));
            myHp.Fuzzify("VERYLOW");
            myNumPlayers.InputValue = (Convert.ToDouble(textBox2.Text));
            myNumPlayers.Fuzzify("NONE");
        
        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "ACTION";
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            fe.Consequent = "ACTION";
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

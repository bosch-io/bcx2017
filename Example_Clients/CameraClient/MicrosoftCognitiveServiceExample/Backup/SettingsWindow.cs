// Decompiled with JetBrains decompiler
// Type: CSharpRuntimeCameo.SettingsWindow
// Assembly: CSharpRuntimeCameo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 00EE9C72-FB3C-47D2-86EA-48DB053A5B3C
// Assembly location: C:\Users\user\Downloads\x86\x86\CSharpRuntimeCameo.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CSharpRuntimeCameo
{
  public class SettingsWindow : Form
  {
    public bool receiveAlarm1 = true;
    public bool receiveAlarm2 = true;
    public bool receiveAlarm3 = true;
    public bool receiveAlarm4 = true;
    public string[] analysisType1 = new string[2]
    {
      "analysis",
      "emotion"
    };
    public string[] analysisType2 = new string[2]
    {
      "analysis",
      "emotion"
    };
    public string[] analysisType3 = new string[2]
    {
      "analysis",
      "emotion"
    };
    public string[] analysisType4 = new string[2]
    {
      "analysis",
      "emotion"
    };
    public string selectedAnalysis1 = "analysis";
    public string selectedAnalysis2 = "analysis";
    public string selectedAnalysis3 = "analysis";
    public string selectedAnalysis4 = "analysis";
    public string pathName = "";
    public Rectangle rect1 = new Rectangle(0, 0, 0, 0);
    public Rectangle rect2 = new Rectangle(0, 0, 0, 0);
    public Rectangle rect3 = new Rectangle(0, 0, 0, 0);
    public Rectangle rect4 = new Rectangle(0, 0, 0, 0);
    private IContainer components;
    private Label label1;
    private Panel panel1;
    private RadioButton radioButton2;
    private RadioButton radioButton1;
    private Button button1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Panel panel2;
    private RadioButton radioButton3;
    private RadioButton radioButton4;
    private Panel panel3;
    private RadioButton radioButton5;
    private RadioButton radioButton6;
    private Panel panel4;
    private RadioButton radioButton7;
    private RadioButton radioButton8;
    private ComboBox comboBox1;
    private BindingSource analysisTypeBindingSource;
    private Label label5;
    private Label label6;
    private ComboBox comboBox2;
    private Label label7;
    private ComboBox comboBox3;
    private Label label8;
    private ComboBox comboBox4;
    private Label label9;
    private TextBox textBox1;
    private Label label10;
    private TextBox textBox1X1;
    private Label label11;
    private Label label12;
    private TextBox textBox1Y1;
    private Label label13;
    private TextBox textBox1Y2;
    private Label label14;
    private TextBox textBox1X2;
    private Label label15;
    private TextBox textBox2Y2;
    private Label label16;
    private TextBox textBox2X2;
    private Label label17;
    private TextBox textBox2Y1;
    private Label label18;
    private TextBox textBox2X1;
    private Label label19;
    private Label label20;
    private TextBox textBox3Y2;
    private Label label21;
    private TextBox textBox3X2;
    private Label label22;
    private TextBox textBox3Y1;
    private Label label23;
    private TextBox textBox3X1;
    private Label label24;
    private Label label25;
    private TextBox textBox4Y2;
    private Label label26;
    private TextBox textBox4X2;
    private Label label27;
    private TextBox textBox4Y1;
    private Label label28;
    private TextBox textBox4X1;
    private Label label29;

    public SettingsWindow()
    {
      this.InitializeComponent();
      this.comboBox1.DataSource = (object) this.analysisType1;
      this.comboBox2.DataSource = (object) this.analysisType2;
      this.comboBox3.DataSource = (object) this.analysisType3;
      this.comboBox4.DataSource = (object) this.analysisType4;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
    }

    private void SettingsWindow_Load(object sender, EventArgs e)
    {
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm1 = this.radioButton1.Checked;
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm1 = this.radioButton1.Checked;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void radioButton4_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm2 = this.radioButton4.Checked;
    }

    private void radioButton3_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm2 = this.radioButton4.Checked;
    }

    private void radioButton6_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm3 = this.radioButton6.Checked;
    }

    private void radioButton5_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm3 = this.radioButton6.Checked;
    }

    private void radioButton8_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm4 = this.radioButton8.Checked;
    }

    private void radioButton7_CheckedChanged(object sender, EventArgs e)
    {
      this.receiveAlarm4 = this.radioButton8.Checked;
    }

    private void label5_Click(object sender, EventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.selectedAnalysis1 = this.comboBox1.SelectedValue.ToString();
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.selectedAnalysis2 = this.comboBox2.SelectedValue.ToString();
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.selectedAnalysis3 = this.comboBox3.SelectedValue.ToString();
    }

    private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.selectedAnalysis4 = this.comboBox4.SelectedValue.ToString();
    }

    private void textBox1_TextChanged_1(object sender, EventArgs e)
    {
      this.pathName = this.textBox1.Text;
    }

    private void label10_Click(object sender, EventArgs e)
    {
    }

    private void textBox1X1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox1X1.Text);
        this.rect1.Location = new Point(Convert.ToInt32(this.textBox1X1.Text), Convert.ToInt32(this.textBox1Y1.Text));
        this.rect1.Width = Convert.ToInt32(this.textBox1X2.Text) - Convert.ToInt32(this.textBox1X1.Text);
        this.rect1.Height = Convert.ToInt32(this.textBox1Y2.Text) - Convert.ToInt32(this.textBox1Y1.Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void textBox1Y1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox1Y1.Text);
        this.rect1.Location = new Point(Convert.ToInt32(this.textBox1X1.Text), Convert.ToInt32(this.textBox1Y1.Text));
        this.rect1.Width = Convert.ToInt32(this.textBox1X2.Text) - Convert.ToInt32(this.textBox1X1.Text);
        this.rect1.Height = Convert.ToInt32(this.textBox1Y2.Text) - Convert.ToInt32(this.textBox1Y1.Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void textBox1X2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox1X2.Text);
        this.rect1.Location = new Point(Convert.ToInt32(this.textBox1X1.Text), Convert.ToInt32(this.textBox1Y1.Text));
        this.rect1.Width = Convert.ToInt32(this.textBox1X2.Text) - Convert.ToInt32(this.textBox1X1.Text);
        this.rect1.Height = Convert.ToInt32(this.textBox1Y2.Text) - Convert.ToInt32(this.textBox1Y1.Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void textBox1Y2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox1Y2.Text);
        this.rect1.Location = new Point(Convert.ToInt32(this.textBox1X1.Text), Convert.ToInt32(this.textBox1Y1.Text));
        this.rect1.Width = Convert.ToInt32(this.textBox1X2.Text) - Convert.ToInt32(this.textBox1X1.Text);
        this.rect1.Height = Convert.ToInt32(this.textBox1Y2.Text) - Convert.ToInt32(this.textBox1Y1.Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void textBox2X1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox2X1.Text);
        this.rect2.Location = new Point(Convert.ToInt32(this.textBox2X1.Text), Convert.ToInt32(this.textBox2Y1.Text));
        this.rect2.Width = Convert.ToInt32(this.textBox2X2.Text) - Convert.ToInt32(this.textBox2X1.Text);
        this.rect2.Height = Convert.ToInt32(this.textBox2Y2.Text) - Convert.ToInt32(this.textBox2Y1.Text);
      }
      catch (Exception ex)
      {
      }
    }

    private void textBox2Y1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox2Y1.Text);
        this.rect2.Location = new Point(Convert.ToInt32(this.textBox2X1.Text), Convert.ToInt32(this.textBox2Y1.Text));
        this.rect2.Width = Convert.ToInt32(this.textBox2X2.Text) - Convert.ToInt32(this.textBox2X1.Text);
        this.rect2.Height = Convert.ToInt32(this.textBox2Y2.Text) - Convert.ToInt32(this.textBox2Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox2X2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox2X2.Text);
        this.rect2.Location = new Point(Convert.ToInt32(this.textBox2X1.Text), Convert.ToInt32(this.textBox2Y1.Text));
        this.rect2.Width = Convert.ToInt32(this.textBox2X2.Text) - Convert.ToInt32(this.textBox2X1.Text);
        this.rect2.Height = Convert.ToInt32(this.textBox2Y2.Text) - Convert.ToInt32(this.textBox2Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox2Y2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox2Y2.Text);
        this.rect2.Location = new Point(Convert.ToInt32(this.textBox2X1.Text), Convert.ToInt32(this.textBox2Y1.Text));
        this.rect2.Width = Convert.ToInt32(this.textBox2X2.Text) - Convert.ToInt32(this.textBox2X1.Text);
        this.rect2.Height = Convert.ToInt32(this.textBox2Y2.Text) - Convert.ToInt32(this.textBox2Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox3X1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox3X1.Text);
        this.rect3.Location = new Point(Convert.ToInt32(this.textBox3X1.Text), Convert.ToInt32(this.textBox3Y1.Text));
        this.rect3.Width = Convert.ToInt32(this.textBox3X2.Text) - Convert.ToInt32(this.textBox3X1.Text);
        this.rect3.Height = Convert.ToInt32(this.textBox3Y2.Text) - Convert.ToInt32(this.textBox3Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox3Y1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox3Y1.Text);
        this.rect3.Location = new Point(Convert.ToInt32(this.textBox3X1.Text), Convert.ToInt32(this.textBox3Y1.Text));
        this.rect3.Width = Convert.ToInt32(this.textBox3X2.Text) - Convert.ToInt32(this.textBox3X1.Text);
        this.rect3.Height = Convert.ToInt32(this.textBox3Y2.Text) - Convert.ToInt32(this.textBox3Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox3X2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox3X2.Text);
        this.rect3.Location = new Point(Convert.ToInt32(this.textBox3X1.Text), Convert.ToInt32(this.textBox3Y1.Text));
        this.rect3.Width = Convert.ToInt32(this.textBox3X2.Text) - Convert.ToInt32(this.textBox3X1.Text);
        this.rect3.Height = Convert.ToInt32(this.textBox3Y2.Text) - Convert.ToInt32(this.textBox3Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox3Y2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox3Y2.Text);
        this.rect3.Location = new Point(Convert.ToInt32(this.textBox3X1.Text), Convert.ToInt32(this.textBox3Y1.Text));
        this.rect3.Width = Convert.ToInt32(this.textBox3X2.Text) - Convert.ToInt32(this.textBox3X1.Text);
        this.rect3.Height = Convert.ToInt32(this.textBox3Y2.Text) - Convert.ToInt32(this.textBox3Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox4X1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox4X1.Text);
        this.rect4.Location = new Point(Convert.ToInt32(this.textBox4X1.Text), Convert.ToInt32(this.textBox4Y1.Text));
        this.rect4.Width = Convert.ToInt32(this.textBox4X2.Text) - Convert.ToInt32(this.textBox4X1.Text);
        this.rect4.Height = Convert.ToInt32(this.textBox4Y2.Text) - Convert.ToInt32(this.textBox4Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox4Y1_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox4Y1.Text);
        this.rect4.Location = new Point(Convert.ToInt32(this.textBox4X1.Text), Convert.ToInt32(this.textBox4Y1.Text));
        this.rect4.Width = Convert.ToInt32(this.textBox4X2.Text) - Convert.ToInt32(this.textBox4X1.Text);
        this.rect4.Height = Convert.ToInt32(this.textBox4Y2.Text) - Convert.ToInt32(this.textBox4Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox4X2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox4X2.Text);
        this.rect4.Location = new Point(Convert.ToInt32(this.textBox4X1.Text), Convert.ToInt32(this.textBox4Y1.Text));
        this.rect4.Width = Convert.ToInt32(this.textBox4X2.Text) - Convert.ToInt32(this.textBox4X1.Text);
        this.rect4.Height = Convert.ToInt32(this.textBox4Y2.Text) - Convert.ToInt32(this.textBox4Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void textBox4Y2_TextChanged(object sender, EventArgs e)
    {
      try
      {
        int.Parse(this.textBox4Y2.Text);
        this.rect4.Location = new Point(Convert.ToInt32(this.textBox4X1.Text), Convert.ToInt32(this.textBox4Y1.Text));
        this.rect4.Width = Convert.ToInt32(this.textBox4X2.Text) - Convert.ToInt32(this.textBox4X1.Text);
        this.rect4.Height = Convert.ToInt32(this.textBox4Y2.Text) - Convert.ToInt32(this.textBox4Y1.Text);
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.label1 = new Label();
      this.panel1 = new Panel();
      this.radioButton2 = new RadioButton();
      this.radioButton1 = new RadioButton();
      this.button1 = new Button();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.panel2 = new Panel();
      this.radioButton3 = new RadioButton();
      this.radioButton4 = new RadioButton();
      this.panel3 = new Panel();
      this.radioButton5 = new RadioButton();
      this.radioButton6 = new RadioButton();
      this.panel4 = new Panel();
      this.radioButton7 = new RadioButton();
      this.radioButton8 = new RadioButton();
      this.comboBox1 = new ComboBox();
      this.analysisTypeBindingSource = new BindingSource(this.components);
      this.label5 = new Label();
      this.label6 = new Label();
      this.comboBox2 = new ComboBox();
      this.label7 = new Label();
      this.comboBox3 = new ComboBox();
      this.label8 = new Label();
      this.comboBox4 = new ComboBox();
      this.label9 = new Label();
      this.textBox1 = new TextBox();
      this.label10 = new Label();
      this.textBox1X1 = new TextBox();
      this.label11 = new Label();
      this.label12 = new Label();
      this.textBox1Y1 = new TextBox();
      this.label13 = new Label();
      this.textBox1Y2 = new TextBox();
      this.label14 = new Label();
      this.textBox1X2 = new TextBox();
      this.label15 = new Label();
      this.textBox2Y2 = new TextBox();
      this.label16 = new Label();
      this.textBox2X2 = new TextBox();
      this.label17 = new Label();
      this.textBox2Y1 = new TextBox();
      this.label18 = new Label();
      this.textBox2X1 = new TextBox();
      this.label19 = new Label();
      this.label20 = new Label();
      this.textBox3Y2 = new TextBox();
      this.label21 = new Label();
      this.textBox3X2 = new TextBox();
      this.label22 = new Label();
      this.textBox3Y1 = new TextBox();
      this.label23 = new Label();
      this.textBox3X1 = new TextBox();
      this.label24 = new Label();
      this.label25 = new Label();
      this.textBox4Y2 = new TextBox();
      this.label26 = new Label();
      this.textBox4X2 = new TextBox();
      this.label27 = new Label();
      this.textBox4Y1 = new TextBox();
      this.label28 = new Label();
      this.textBox4X1 = new TextBox();
      this.label29 = new Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      ((ISupportInitialize) this.analysisTypeBindingSource).BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(14, 12);
      this.label1.Name = "label1";
      this.label1.Size = new Size(105, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Receive IVA Alarm 1";
      this.panel1.Controls.Add((Control) this.radioButton2);
      this.panel1.Controls.Add((Control) this.radioButton1);
      this.panel1.Location = new Point(12, 28);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(99, 45);
      this.panel1.TabIndex = 3;
      this.radioButton2.AutoSize = true;
      this.radioButton2.Location = new Point(5, 26);
      this.radioButton2.Name = "radioButton2";
      this.radioButton2.Size = new Size(39, 17);
      this.radioButton2.TabIndex = 1;
      this.radioButton2.Text = "Off";
      this.radioButton2.UseVisualStyleBackColor = true;
      this.radioButton2.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
      this.radioButton1.AutoSize = true;
      this.radioButton1.Checked = true;
      this.radioButton1.Location = new Point(5, 3);
      this.radioButton1.Name = "radioButton1";
      this.radioButton1.Size = new Size(39, 17);
      this.radioButton1.TabIndex = 0;
      this.radioButton1.TabStop = true;
      this.radioButton1.Text = "On";
      this.radioButton1.UseVisualStyleBackColor = true;
      this.radioButton1.CheckedChanged += new EventHandler(this.radioButton1_CheckedChanged);
      this.button1.Location = new Point(95, 370);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "Close";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(14, 87);
      this.label2.Name = "label2";
      this.label2.Size = new Size(105, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Receive IVA Alarm 2";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(14, 166);
      this.label3.Name = "label3";
      this.label3.Size = new Size(105, 13);
      this.label3.TabIndex = 6;
      this.label3.Text = "Receive IVA Alarm 3";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(14, 240);
      this.label4.Name = "label4";
      this.label4.Size = new Size(105, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Receive IVA Alarm 4";
      this.panel2.Controls.Add((Control) this.radioButton3);
      this.panel2.Controls.Add((Control) this.radioButton4);
      this.panel2.Location = new Point(12, 104);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(99, 45);
      this.panel2.TabIndex = 8;
      this.radioButton3.AutoSize = true;
      this.radioButton3.Location = new Point(5, 26);
      this.radioButton3.Name = "radioButton3";
      this.radioButton3.Size = new Size(39, 17);
      this.radioButton3.TabIndex = 1;
      this.radioButton3.Text = "Off";
      this.radioButton3.UseVisualStyleBackColor = true;
      this.radioButton3.CheckedChanged += new EventHandler(this.radioButton3_CheckedChanged);
      this.radioButton4.AutoSize = true;
      this.radioButton4.Checked = true;
      this.radioButton4.Location = new Point(5, 3);
      this.radioButton4.Name = "radioButton4";
      this.radioButton4.Size = new Size(39, 17);
      this.radioButton4.TabIndex = 0;
      this.radioButton4.TabStop = true;
      this.radioButton4.Text = "On";
      this.radioButton4.UseVisualStyleBackColor = true;
      this.radioButton4.CheckedChanged += new EventHandler(this.radioButton4_CheckedChanged);
      this.panel3.Controls.Add((Control) this.radioButton5);
      this.panel3.Controls.Add((Control) this.radioButton6);
      this.panel3.Location = new Point(12, 182);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(99, 45);
      this.panel3.TabIndex = 4;
      this.radioButton5.AutoSize = true;
      this.radioButton5.Location = new Point(5, 26);
      this.radioButton5.Name = "radioButton5";
      this.radioButton5.Size = new Size(39, 17);
      this.radioButton5.TabIndex = 1;
      this.radioButton5.Text = "Off";
      this.radioButton5.UseVisualStyleBackColor = true;
      this.radioButton5.CheckedChanged += new EventHandler(this.radioButton5_CheckedChanged);
      this.radioButton6.AutoSize = true;
      this.radioButton6.Checked = true;
      this.radioButton6.Location = new Point(5, 3);
      this.radioButton6.Name = "radioButton6";
      this.radioButton6.Size = new Size(39, 17);
      this.radioButton6.TabIndex = 0;
      this.radioButton6.TabStop = true;
      this.radioButton6.Text = "On";
      this.radioButton6.UseVisualStyleBackColor = true;
      this.radioButton6.CheckedChanged += new EventHandler(this.radioButton6_CheckedChanged);
      this.panel4.Controls.Add((Control) this.radioButton7);
      this.panel4.Controls.Add((Control) this.radioButton8);
      this.panel4.Location = new Point(12, 256);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(99, 45);
      this.panel4.TabIndex = 4;
      this.radioButton7.AutoSize = true;
      this.radioButton7.Location = new Point(5, 26);
      this.radioButton7.Name = "radioButton7";
      this.radioButton7.Size = new Size(39, 17);
      this.radioButton7.TabIndex = 1;
      this.radioButton7.Text = "Off";
      this.radioButton7.UseVisualStyleBackColor = true;
      this.radioButton7.CheckedChanged += new EventHandler(this.radioButton7_CheckedChanged);
      this.radioButton8.AutoSize = true;
      this.radioButton8.Checked = true;
      this.radioButton8.Location = new Point(5, 3);
      this.radioButton8.Name = "radioButton8";
      this.radioButton8.Size = new Size(39, 17);
      this.radioButton8.TabIndex = 0;
      this.radioButton8.TabStop = true;
      this.radioButton8.Text = "On";
      this.radioButton8.UseVisualStyleBackColor = true;
      this.radioButton8.CheckedChanged += new EventHandler(this.radioButton8_CheckedChanged);
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(140, 27);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(121, 21);
      this.comboBox1.TabIndex = 9;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(137, 12);
      this.label5.Name = "label5";
      this.label5.Size = new Size(72, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Analysis Type";
      this.label5.Click += new EventHandler(this.label5_Click);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(137, 87);
      this.label6.Name = "label6";
      this.label6.Size = new Size(72, 13);
      this.label6.TabIndex = 12;
      this.label6.Text = "Analysis Type";
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Location = new Point(140, 102);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new Size(121, 21);
      this.comboBox2.TabIndex = 11;
      this.comboBox2.SelectedIndexChanged += new EventHandler(this.comboBox2_SelectedIndexChanged);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(137, 166);
      this.label7.Name = "label7";
      this.label7.Size = new Size(72, 13);
      this.label7.TabIndex = 14;
      this.label7.Text = "Analysis Type";
      this.comboBox3.FormattingEnabled = true;
      this.comboBox3.Location = new Point(140, 181);
      this.comboBox3.Name = "comboBox3";
      this.comboBox3.Size = new Size(121, 21);
      this.comboBox3.TabIndex = 13;
      this.comboBox3.SelectedIndexChanged += new EventHandler(this.comboBox3_SelectedIndexChanged);
      this.label8.AutoSize = true;
      this.label8.Location = new Point(137, 240);
      this.label8.Name = "label8";
      this.label8.Size = new Size(72, 13);
      this.label8.TabIndex = 16;
      this.label8.Text = "Analysis Type";
      this.comboBox4.FormattingEnabled = true;
      this.comboBox4.Location = new Point(140, (int) byte.MaxValue);
      this.comboBox4.Name = "comboBox4";
      this.comboBox4.Size = new Size(121, 21);
      this.comboBox4.TabIndex = 15;
      this.comboBox4.SelectedIndexChanged += new EventHandler(this.comboBox4_SelectedIndexChanged);
      this.label9.AutoSize = true;
      this.label9.Location = new Point(15, 314);
      this.label9.Name = "label9";
      this.label9.Size = new Size(88, 13);
      this.label9.TabIndex = 17;
      this.label9.Text = "Path to save files";
      this.textBox1.Location = new Point(12, 330);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(260, 20);
      this.textBox1.TabIndex = 18;
      this.textBox1.TextChanged += new EventHandler(this.textBox1_TextChanged_1);
      this.label10.AutoSize = true;
      this.label10.Location = new Point(284, 12);
      this.label10.Name = "label10";
      this.label10.Size = new Size(29, 13);
      this.label10.TabIndex = 19;
      this.label10.Text = "Crop";
      this.label10.Click += new EventHandler(this.label10_Click);
      this.textBox1X1.Location = new Point(310, 27);
      this.textBox1X1.Name = "textBox1X1";
      this.textBox1X1.Size = new Size(100, 20);
      this.textBox1X1.TabIndex = 20;
      this.textBox1X1.Text = "0";
      this.textBox1X1.TextChanged += new EventHandler(this.textBox1X1_TextChanged);
      this.label11.AutoSize = true;
      this.label11.Location = new Point(284, 30);
      this.label11.Name = "label11";
      this.label11.Size = new Size(20, 13);
      this.label11.TabIndex = 21;
      this.label11.Text = "X1";
      this.label12.AutoSize = true;
      this.label12.Location = new Point(419, 30);
      this.label12.Name = "label12";
      this.label12.Size = new Size(20, 13);
      this.label12.TabIndex = 23;
      this.label12.Text = "Y1";
      this.textBox1Y1.Location = new Point(445, 27);
      this.textBox1Y1.Name = "textBox1Y1";
      this.textBox1Y1.Size = new Size(100, 20);
      this.textBox1Y1.TabIndex = 22;
      this.textBox1Y1.Text = "0";
      this.textBox1Y1.TextChanged += new EventHandler(this.textBox1Y1_TextChanged);
      this.label13.AutoSize = true;
      this.label13.Location = new Point(419, 54);
      this.label13.Name = "label13";
      this.label13.Size = new Size(20, 13);
      this.label13.TabIndex = 27;
      this.label13.Text = "Y2";
      this.textBox1Y2.Location = new Point(445, 51);
      this.textBox1Y2.Name = "textBox1Y2";
      this.textBox1Y2.Size = new Size(100, 20);
      this.textBox1Y2.TabIndex = 26;
      this.textBox1Y2.Text = "0";
      this.textBox1Y2.TextChanged += new EventHandler(this.textBox1Y2_TextChanged);
      this.label14.AutoSize = true;
      this.label14.Location = new Point(284, 54);
      this.label14.Name = "label14";
      this.label14.Size = new Size(20, 13);
      this.label14.TabIndex = 25;
      this.label14.Text = "X2";
      this.textBox1X2.Location = new Point(310, 51);
      this.textBox1X2.Name = "textBox1X2";
      this.textBox1X2.Size = new Size(100, 20);
      this.textBox1X2.TabIndex = 24;
      this.textBox1X2.Text = "0";
      this.textBox1X2.TextChanged += new EventHandler(this.textBox1X2_TextChanged);
      this.label15.AutoSize = true;
      this.label15.Location = new Point(419, 130);
      this.label15.Name = "label15";
      this.label15.Size = new Size(20, 13);
      this.label15.TabIndex = 36;
      this.label15.Text = "Y2";
      this.textBox2Y2.Location = new Point(445, (int) sbyte.MaxValue);
      this.textBox2Y2.Name = "textBox2Y2";
      this.textBox2Y2.Size = new Size(100, 20);
      this.textBox2Y2.TabIndex = 35;
      this.textBox2Y2.Text = "0";
      this.textBox2Y2.TextChanged += new EventHandler(this.textBox2Y2_TextChanged);
      this.label16.AutoSize = true;
      this.label16.Location = new Point(284, 130);
      this.label16.Name = "label16";
      this.label16.Size = new Size(20, 13);
      this.label16.TabIndex = 34;
      this.label16.Text = "X2";
      this.textBox2X2.Location = new Point(310, (int) sbyte.MaxValue);
      this.textBox2X2.Name = "textBox2X2";
      this.textBox2X2.Size = new Size(100, 20);
      this.textBox2X2.TabIndex = 33;
      this.textBox2X2.Text = "0";
      this.textBox2X2.TextChanged += new EventHandler(this.textBox2X2_TextChanged);
      this.label17.AutoSize = true;
      this.label17.Location = new Point(419, 106);
      this.label17.Name = "label17";
      this.label17.Size = new Size(20, 13);
      this.label17.TabIndex = 32;
      this.label17.Text = "Y1";
      this.textBox2Y1.Location = new Point(445, 103);
      this.textBox2Y1.Name = "textBox2Y1";
      this.textBox2Y1.Size = new Size(100, 20);
      this.textBox2Y1.TabIndex = 31;
      this.textBox2Y1.Text = "0";
      this.textBox2Y1.TextChanged += new EventHandler(this.textBox2Y1_TextChanged);
      this.label18.AutoSize = true;
      this.label18.Location = new Point(284, 106);
      this.label18.Name = "label18";
      this.label18.Size = new Size(20, 13);
      this.label18.TabIndex = 30;
      this.label18.Text = "X1";
      this.textBox2X1.Location = new Point(310, 103);
      this.textBox2X1.Name = "textBox2X1";
      this.textBox2X1.Size = new Size(100, 20);
      this.textBox2X1.TabIndex = 29;
      this.textBox2X1.Text = "0";
      this.textBox2X1.TextChanged += new EventHandler(this.textBox2X1_TextChanged);
      this.label19.AutoSize = true;
      this.label19.Location = new Point(284, 88);
      this.label19.Name = "label19";
      this.label19.Size = new Size(29, 13);
      this.label19.TabIndex = 28;
      this.label19.Text = "Crop";
      this.label20.AutoSize = true;
      this.label20.Location = new Point(419, 212);
      this.label20.Name = "label20";
      this.label20.Size = new Size(20, 13);
      this.label20.TabIndex = 45;
      this.label20.Text = "Y2";
      this.textBox3Y2.Location = new Point(445, 209);
      this.textBox3Y2.Name = "textBox3Y2";
      this.textBox3Y2.Size = new Size(100, 20);
      this.textBox3Y2.TabIndex = 44;
      this.textBox3Y2.Text = "0";
      this.textBox3Y2.TextChanged += new EventHandler(this.textBox3Y2_TextChanged);
      this.label21.AutoSize = true;
      this.label21.Location = new Point(284, 212);
      this.label21.Name = "label21";
      this.label21.Size = new Size(20, 13);
      this.label21.TabIndex = 43;
      this.label21.Text = "X2";
      this.textBox3X2.Location = new Point(310, 209);
      this.textBox3X2.Name = "textBox3X2";
      this.textBox3X2.Size = new Size(100, 20);
      this.textBox3X2.TabIndex = 42;
      this.textBox3X2.Text = "0";
      this.textBox3X2.TextChanged += new EventHandler(this.textBox3X2_TextChanged);
      this.label22.AutoSize = true;
      this.label22.Location = new Point(419, 188);
      this.label22.Name = "label22";
      this.label22.Size = new Size(20, 13);
      this.label22.TabIndex = 41;
      this.label22.Text = "Y1";
      this.textBox3Y1.Location = new Point(445, 185);
      this.textBox3Y1.Name = "textBox3Y1";
      this.textBox3Y1.Size = new Size(100, 20);
      this.textBox3Y1.TabIndex = 40;
      this.textBox3Y1.Text = "0";
      this.textBox3Y1.TextChanged += new EventHandler(this.textBox3Y1_TextChanged);
      this.label23.AutoSize = true;
      this.label23.Location = new Point(284, 188);
      this.label23.Name = "label23";
      this.label23.Size = new Size(20, 13);
      this.label23.TabIndex = 39;
      this.label23.Text = "X1";
      this.textBox3X1.Location = new Point(310, 185);
      this.textBox3X1.Name = "textBox3X1";
      this.textBox3X1.Size = new Size(100, 20);
      this.textBox3X1.TabIndex = 38;
      this.textBox3X1.Text = "0";
      this.textBox3X1.TextChanged += new EventHandler(this.textBox3X1_TextChanged);
      this.label24.AutoSize = true;
      this.label24.Location = new Point(284, 170);
      this.label24.Name = "label24";
      this.label24.Size = new Size(29, 13);
      this.label24.TabIndex = 37;
      this.label24.Text = "Crop";
      this.label25.AutoSize = true;
      this.label25.Location = new Point(419, 282);
      this.label25.Name = "label25";
      this.label25.Size = new Size(20, 13);
      this.label25.TabIndex = 54;
      this.label25.Text = "Y2";
      this.textBox4Y2.Location = new Point(445, 279);
      this.textBox4Y2.Name = "textBox4Y2";
      this.textBox4Y2.Size = new Size(100, 20);
      this.textBox4Y2.TabIndex = 53;
      this.textBox4Y2.Text = "0";
      this.textBox4Y2.TextChanged += new EventHandler(this.textBox4Y2_TextChanged);
      this.label26.AutoSize = true;
      this.label26.Location = new Point(284, 282);
      this.label26.Name = "label26";
      this.label26.Size = new Size(20, 13);
      this.label26.TabIndex = 52;
      this.label26.Text = "X2";
      this.textBox4X2.Location = new Point(310, 279);
      this.textBox4X2.Name = "textBox4X2";
      this.textBox4X2.Size = new Size(100, 20);
      this.textBox4X2.TabIndex = 51;
      this.textBox4X2.Text = "0";
      this.textBox4X2.TextChanged += new EventHandler(this.textBox4X2_TextChanged);
      this.label27.AutoSize = true;
      this.label27.Location = new Point(419, 258);
      this.label27.Name = "label27";
      this.label27.Size = new Size(20, 13);
      this.label27.TabIndex = 50;
      this.label27.Text = "Y1";
      this.textBox4Y1.Location = new Point(445, (int) byte.MaxValue);
      this.textBox4Y1.Name = "textBox4Y1";
      this.textBox4Y1.Size = new Size(100, 20);
      this.textBox4Y1.TabIndex = 49;
      this.textBox4Y1.Text = "0";
      this.textBox4Y1.TextChanged += new EventHandler(this.textBox4Y1_TextChanged);
      this.label28.AutoSize = true;
      this.label28.Location = new Point(284, 258);
      this.label28.Name = "label28";
      this.label28.Size = new Size(20, 13);
      this.label28.TabIndex = 48;
      this.label28.Text = "X1";
      this.textBox4X1.Location = new Point(310, (int) byte.MaxValue);
      this.textBox4X1.Name = "textBox4X1";
      this.textBox4X1.Size = new Size(100, 20);
      this.textBox4X1.TabIndex = 47;
      this.textBox4X1.Text = "0";
      this.textBox4X1.TextChanged += new EventHandler(this.textBox4X1_TextChanged);
      this.label29.AutoSize = true;
      this.label29.Location = new Point(284, 240);
      this.label29.Name = "label29";
      this.label29.Size = new Size(29, 13);
      this.label29.TabIndex = 46;
      this.label29.Text = "Crop";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(574, 405);
      this.ControlBox = false;
      this.Controls.Add((Control) this.label25);
      this.Controls.Add((Control) this.textBox4Y2);
      this.Controls.Add((Control) this.label26);
      this.Controls.Add((Control) this.textBox4X2);
      this.Controls.Add((Control) this.label27);
      this.Controls.Add((Control) this.textBox4Y1);
      this.Controls.Add((Control) this.label28);
      this.Controls.Add((Control) this.textBox4X1);
      this.Controls.Add((Control) this.label29);
      this.Controls.Add((Control) this.label20);
      this.Controls.Add((Control) this.textBox3Y2);
      this.Controls.Add((Control) this.label21);
      this.Controls.Add((Control) this.textBox3X2);
      this.Controls.Add((Control) this.label22);
      this.Controls.Add((Control) this.textBox3Y1);
      this.Controls.Add((Control) this.label23);
      this.Controls.Add((Control) this.textBox3X1);
      this.Controls.Add((Control) this.label24);
      this.Controls.Add((Control) this.label15);
      this.Controls.Add((Control) this.textBox2Y2);
      this.Controls.Add((Control) this.label16);
      this.Controls.Add((Control) this.textBox2X2);
      this.Controls.Add((Control) this.label17);
      this.Controls.Add((Control) this.textBox2Y1);
      this.Controls.Add((Control) this.label18);
      this.Controls.Add((Control) this.textBox2X1);
      this.Controls.Add((Control) this.label19);
      this.Controls.Add((Control) this.label13);
      this.Controls.Add((Control) this.textBox1Y2);
      this.Controls.Add((Control) this.label14);
      this.Controls.Add((Control) this.textBox1X2);
      this.Controls.Add((Control) this.label12);
      this.Controls.Add((Control) this.textBox1Y1);
      this.Controls.Add((Control) this.label11);
      this.Controls.Add((Control) this.textBox1X1);
      this.Controls.Add((Control) this.label10);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.comboBox4);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.comboBox3);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.comboBox2);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.panel4);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.label1);
      this.Name = "SettingsWindow";
      this.Text = "Settings";
      this.Load += new EventHandler(this.SettingsWindow_Load);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      ((ISupportInitialize) this.analysisTypeBindingSource).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}

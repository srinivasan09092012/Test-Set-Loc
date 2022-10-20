using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace UnityToAutoFac
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(richTextBox2.Text);
            }
            catch(Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

            try
            {
                XmlNodeList registers = xmlDoc.GetElementsByTagName("register");
                string finalText = string.Empty;

                finalText = "{\"components\" : [ ";

                string autoFactext = string.Empty;

                if(registers.Count == 0)
                {
                    return;
                }

                foreach (XmlNode node in registers)
                {
                    string type = node.Attributes["type"].Value;
                    string mapTo = node.Attributes["mapTo"]?.Value;

                    if (string.IsNullOrEmpty(mapTo))
                    {
                        mapTo = type;
                    }

                    XmlAttribute attribute = node.Attributes["name"];


                    autoFactext += "{\"type\" : \"" + mapTo + "\",";

                    autoFactext += "\"services\" : [ { \"type\" : \"" + type + "\"";

                    if (attribute != null)
                    {
                        autoFactext += ", \"key\" : \"" + attribute.Value + "\"";
                    }
                    autoFactext += "}],";

                    XmlNode valueNode = node.FirstChild;
                    if (valueNode != null)
                    {
                        autoFactext += getinstanceScope(valueNode);                     
                    }


                    XmlDocument registerdoc = new XmlDocument();
                    registerdoc.LoadXml(node.OuterXml);

                    XmlNodeList paramList = registerdoc.GetElementsByTagName("param");
                    if (paramList?.Count > 0)
                    {
                        autoFactext = prepareParams(autoFactext, paramList);
                    }

                    autoFactext += "},";

                }
                autoFactext = autoFactext.Remove(autoFactext.Length - 1);

                finalText = finalText + autoFactext + "]}";

                richTextBox1.Text = JsonConvert.DeserializeObject(finalText).ToString();
            }
            catch(Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }
        }

        private string prepareParams(string autoFactext, XmlNodeList paramList)
        {
            autoFactext += "\"parameters\" : { ";

            foreach (XmlNode param in paramList)
            {
                string name = param.Attributes["name"].Value;
                string dependencyName = param.Attributes["dependencyName"]?.Value;
                if(string.IsNullOrEmpty(dependencyName))
                {
                    XmlNode valueNode1 = param.FirstChild;
                    string value1 = valueNode1.Attributes[0].Value;
                    if (value1.Contains("\\"))
                    {
                        value1 = value1.Replace(@"\", @"\\");
                    }

                    autoFactext += "\"" + name + "\" : ";
                    autoFactext += "\"" + value1 + "\",";
                }               
            }
            autoFactext = autoFactext.Remove(autoFactext.Length - 1);
            autoFactext += "}";

            return autoFactext;
        }

        private string getinstanceScope(XmlNode valueNode)
        {
            string value = valueNode.Attributes[0].Value;
            string retValue = string.Empty;
           
            if (value == "transient")
            {
                retValue += "\"instanceScope\" : \"per-dependency\",";
            }
            if (value == "singleton")
            {
                retValue += "\"instanceScope\" : \"single-instance\",";
            }
            if (value == "container")
            {
                retValue += "\"instanceScope\" : \"per-lifetime-scope\",";
            }
            return retValue;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string actualValue = richTextBox1.Text;
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                string value = string.Empty;
                string result = actualValue;
                try
                {
                    value = JValue.Parse(result).ToString(Formatting.Indented);

                }
                catch
                {
                    value = result;
                }
                richTextBox1.Text = value;
                var test = this.IsValidJson(value);
                if (test)
                {
                    label1.Text = "Valid JSON";
                    label1.ForeColor = Color.Green;
                    label1.Visible = true;
                }
                else
                {
                    label1.Text = "Invalid JSON";
                    label1.ForeColor = Color.Red;
                    label1.Visible = true;
                }
            }
        }

        private bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(richTextBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\UA3\PLI Team Implementation\IOC - Containers"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.xml"); //Getting Text files
            
            comboBox1.Items.Add("Select");
            comboBox1.Text = "Select";
            comboBox2.Items.Add("Select");
            comboBox2.Text = "Select";
            HashSet<string> set = new HashSet<string>();
            foreach (FileInfo file in Files)
            {
                set.Add(file.Name.Split('-')[0]);
            }

            comboBox1.Items.AddRange(set.ToArray());

            comboBox2.Items.Add("UX");
            comboBox2.Items.Add("BAS");
            comboBox2.Items.Add("Batch");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplications();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadApplications();
        }

        private void LoadApplications()
        {
            string Module = comboBox1.SelectedItem?.ToString();
            string Type = comboBox2.SelectedItem?.ToString();
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Select");
            comboBox3.Text = "Select";

            if (string.IsNullOrEmpty(Module) || string.IsNullOrEmpty(Type))
            {
                return;
            }
            if(Type == "UX")
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(@"C:\UA3\PLI Team Implementation\IOC - Containers\" + Module + "-" +Type +".xml" );
                richTextBox2.Text = xmlDoc.OuterXml;
                return;

            }
            DirectoryInfo d = new DirectoryInfo(@"C:\UA3\PLI Team Implementation\IOC - Containers"); 

            FileInfo[] Files = d.GetFiles(Module + "-" +Type + "*.xml"); 
            
            HashSet<string> set = new HashSet<string>();
            foreach (FileInfo file in Files)
            {
                
                if(file.Name.Contains(Module))
                {
                    List<string> strings = file.Name.Split('-').ToList();
                    if(strings.Count > 2)
                    set.Add(strings[2].Replace(".xml", ""));
                }

              
            }

            comboBox3.Items.AddRange(set.ToArray());
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedItem?.ToString() == "Select")
            {
                return;
            }
            string Module = comboBox1.SelectedItem?.ToString();
            string Type = comboBox2.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(Module) || string.IsNullOrEmpty(Type))
            {
                return;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\UA3\PLI Team Implementation\IOC - Containers\" + Module + "-" + Type + "-" + comboBox3.SelectedItem?.ToString() + ".xml");
            richTextBox2.Text = xmlDoc.OuterXml;
            return;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string oldValue = richTextBox1.Text;
            string oldNameSpaceValue = OldNameSpaceTxt.Text;
            string newNameSpaceValue = NewNameSpaceTxt.Text;

            string newValue = oldValue.Replace(oldNameSpaceValue, newNameSpaceValue);
            richTextBox1.Text = JsonConvert.DeserializeObject(newValue).ToString();
        }
    }
}
using Newtonsoft.Json.Linq;
using RESTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project3
{
    public partial class Form1 : Form
    {
        RestUtil rest = null;       // Reference to the REST DLL
        Degrees degree = null;      // Reference to the  degrees
        DegreeModal deMoal = null;  // Reference to the degree pop up window

        Minor mins = null;          // Reference to the minor
        MinorModal mModal = null;   //Reference to the pop up window

        Employment emp = null;      //Reference to the employment

        People people = null;       //Reference to the faculty and staff
        PeopleModal peopleM = null; //Reference to the pop up window

        Research research = null;   //Reference to the research areass and research faculty

        Resources resou = null;     //Reference to the resource

        News news = null;           //Reference to the news

        Foot foot = null;           //Reference to the footer


        public Form1()
        {
            InitializeComponent();
            rest = new RestUtil("http://ist.rit.edu/api");

            aboutInfo();    //The information of about
            degreeInfo();   //The information about degrees
            minorInfo();    //The information about minors
            employInfo();   //the information about employment
            peopleInfo();   //The information about faculty and staff
            researchInfo(); //The information about research
            resourceInfo(); //The information about resource
            newsInfo();     //The information about news
            footInfo();     //The information about footer


        }

        //The about API
        public void aboutInfo()
        {
            // Get About from the API
            string jsonAbout = rest.getRESTData("/about/");

            // Convert the JSON to a C# object
            About about = JToken.Parse(jsonAbout).ToObject<About>();

            //display the information of about
            lb_title.Text = about.title;
            tb_description.Text = about.description;
            tb_quote.Text = about.quote;
            lb_quoteAuthor.Text = about.quoteAuthor;
          
        }

        
        //The degree API
        public void degreeInfo()
        {
            //Get Degrees from the API
            string jsonDegrees = rest.getRESTData("/degrees/");

            // Convert the JSON to a C# object
           degree = JToken.Parse(jsonDegrees).ToObject<Degrees>();

            //Create dynamic button and diplay the name of undergraduate degree
           for(int i=0; i< degree.undergraduate.Count; i++)
            {  
                Button btDegree = new Button();
                btDegree.Name = degree.undergraduate[i].degreeName;
                btDegree.Text = degree.undergraduate[i].title;
                btDegree.Size = new Size(200,200);
                btDegree.BackColor = Color.Snow;
                btDegree.Cursor = Cursors.Hand; 

                btDegree.Location = new Point(110 + (250 * i), 80);  
                btDegree.Click += new EventHandler(degreeButtonClick);

                this.tp_degree.Controls.Add(btDegree);             
            }

            //Create dynamic button and diplay the name of graduate degree
            for (int i=0; i< degree.graduate.Count - 1; i++)
            {
              
                Button btGDegree = new Button();
                btGDegree.Name = degree.graduate[i].degreeName;
                btGDegree.Text = degree.graduate[i].title;
                btGDegree.Size = new Size(200, 200);
                btGDegree.BackColor = Color.Snow;
                btGDegree.Cursor = Cursors.Hand;

                btGDegree.Location = new Point(110 + (250 * i),370);

                btGDegree.Click += new EventHandler(gDegreeButtonClick);


                this.tp_degree.Controls.Add(btGDegree);
            }

            //Show the available Certificates
            lb_graduateAd.Text = degree.graduate[degree.graduate.Count - 1].degreeName;

            //Create the dynamic labels
            for (int z = 0; z < degree.graduate[degree.graduate.Count - 1].availableCertificates.Count; z++)
            {
                Label lb_ava = new Label();
                lb_ava.Text = degree.graduate[degree.graduate.Count - 1].availableCertificates[z];
                lb_ava.Location = new Point(260, 630+(30*z));
                lb_ava.Size = new Size(500,30);
                Console.WriteLine(lb_ava);
                this.tp_degree.Controls.Add(lb_ava);
            }

        }

        //Click the undergraduate degree name button will show a modal window 
        public void degreeButtonClick(object sender, EventArgs e)
        {
            deMoal = new DegreeModal();

            string dName = null;
            dName = (sender as Button).Name;

            getSingleDegree(dName);

        //    Console.WriteLine(dName);

            deMoal.ShowDialog();

        }

        //Click the graduate degree name button will show a modal window
        public void gDegreeButtonClick(object sender, EventArgs e)
        {
            deMoal = new DegreeModal();

            string gName = null;

            gName = (sender as Button).Name;

            getGSingleDegree(gName);
            deMoal.ShowDialog();
        }

        //Display the special undergraduate name information on pop up window
        public void getSingleDegree(string degName)
        {

            Undergraduate under = degree.undergraduate.Find(x => x.degreeName == degName);
            //Create dynamic label
            Label lb_dTitle = new Label();
            lb_dTitle.Text = under.title;
            lb_dTitle.Size = new Size(500, 60);
            lb_dTitle.Location = new Point(130,26);
            lb_dTitle.Font = new Font(lb_dTitle.Font.FontFamily, 14);

            //Create dynamic rich text box
            RichTextBox rtb_dDescription = new RichTextBox();
            rtb_dDescription.Text = under.description;
            rtb_dDescription.Location = new Point(100,120);
            rtb_dDescription.Size = new Size(400,120);
            rtb_dDescription.Font = new Font(rtb_dDescription.Font.FontFamily, 14);
            rtb_dDescription.ReadOnly = true;
            rtb_dDescription.BackColor = Color.Snow;

            //Create dynamic label
            Label lb_cTitle = new Label();
            lb_cTitle.Text = "Concentration";
            lb_cTitle.Location = new Point(100,280);
            lb_cTitle.Size = new Size(500,60);
            lb_cTitle.Font = new Font(lb_dTitle.Font.FontFamily, 14);

            //Create dynamic rich text box
            RichTextBox rtb_concenteration = new RichTextBox();
            rtb_concenteration.Location = new Point(100, 340);
            rtb_concenteration.Size = new Size(400,140);
            rtb_concenteration.ReadOnly = true;
            rtb_concenteration.BackColor = Color.Snow;
            rtb_concenteration.Font = new Font(rtb_concenteration.Font.FontFamily,13);

            for(int i = 0; i < under.concentrations.Count; i++)
            {
                rtb_concenteration.Text += "- "+ under.concentrations[i]+"\n";

            }


            deMoal.Controls.Add(lb_dTitle);
            deMoal.Controls.Add(rtb_dDescription);
            deMoal.Controls.Add(lb_cTitle);
            deMoal.Controls.Add(rtb_concenteration);

           // MessageBox.Show(under.description);
        }


        //Display the special graduate name information on pop up window
        public void getGSingleDegree(String gDegreeName)
        {
            Graduate grad = degree.graduate.Find(x => x.degreeName == gDegreeName);
            //Create dynamic label
            Label lb_gTitle = new Label();
            lb_gTitle.Text =grad.title;
            lb_gTitle.Size = new Size(500, 60);
            lb_gTitle.Location = new Point(130, 26);
            lb_gTitle.Font = new Font(lb_gTitle.Font.FontFamily, 14);

            //Create dynamic rich text box
            RichTextBox rtb_gDescription = new RichTextBox();
            rtb_gDescription.Text = grad.description;
            rtb_gDescription.Location = new Point(100, 120);
            rtb_gDescription.Size = new Size(400, 120);
            rtb_gDescription.Font = new Font(rtb_gDescription.Font.FontFamily, 14);
            rtb_gDescription.ReadOnly = true;
            rtb_gDescription.BackColor = Color.Snow;

            //Create dynamic label
            Label lb_cGTitle = new Label();
            lb_cGTitle.Text = "Concentration";
            lb_cGTitle.Location = new Point(100, 280);
            lb_cGTitle.Size = new Size(500, 60);
            lb_cGTitle.Font = new Font(lb_cGTitle.Font.FontFamily, 14);

            //Create dynamic rich text box
            RichTextBox rtb_Gconcenteration = new RichTextBox();
            rtb_Gconcenteration.Location = new Point(100, 340);
            rtb_Gconcenteration.Size = new Size(400, 140);
            rtb_Gconcenteration.ReadOnly = true;
            rtb_Gconcenteration.BackColor = Color.Snow;
            rtb_Gconcenteration.Font = new Font(rtb_Gconcenteration.Font.FontFamily, 13);

            for (int i = 0; i < grad.concentrations.Count; i++)
            {
                rtb_Gconcenteration.Text += "- "+grad.concentrations[i] + "\n";

            }


            deMoal.Controls.Add(lb_gTitle);
            deMoal.Controls.Add(rtb_gDescription);
            deMoal.Controls.Add(lb_cGTitle);
            deMoal.Controls.Add(rtb_Gconcenteration);

        }

        //The minor API
        public void minorInfo()
        {
            // Get Minors from the API
            string jsonMinor = rest.getRESTData("/minors/");

            // Convert the JSON to a C# object
            mins = JToken.Parse(jsonMinor).ToObject<Minor>();

            for (int i = 0; i < mins.UgMinors.Count; i++)
            {
                //Create dynamic button
                Button bt_minors = new Button();
                bt_minors.Name = mins.UgMinors[i].name;
                bt_minors.Text = mins.UgMinors[i].title;
                bt_minors.Size = new Size(200, 200);
                bt_minors.BackColor = Color.Snow;
                bt_minors.Cursor = Cursors.Hand;
                bt_minors.Margin = new Padding(10);

                bt_minors.Click += new EventHandler(minorButtonClick);

                flp_minor.Controls.Add(bt_minors);
            }
        }

        //Click the minor name button will show a modal window 
        public void minorButtonClick(object sender, EventArgs e)
        {
            mModal = new MinorModal();
            string mName = null;
            mName = (sender as Button).Name;

            getSingleMinor(mName);

            mModal.ShowDialog();

        }

        //Display the special minor name information on pop up window
        public void getSingleMinor(string minName)
        {
            UgMinor uminor = mins.UgMinors.Find(x => x.name == minName);

            //Create dynamic label
            Label lb_min = new Label();
            lb_min.Text = uminor.title;
            lb_min.Location = new Point(180,20);
            lb_min.Size = new Size(600,40);
            lb_min.Font = new Font(lb_min.Font.FontFamily, 14);

            //Creat dynamic rich text box 
            RichTextBox rtb_mDescription = new RichTextBox();
            rtb_mDescription.Text = uminor.description;
            rtb_mDescription.Location = new Point(100, 70);
            rtb_mDescription.Size = new Size(600, 220);
            rtb_mDescription.Font = new Font(rtb_mDescription.Font.FontFamily, 12);
            rtb_mDescription.ReadOnly = true;
            rtb_mDescription.BackColor = Color.Snow;

            //Create dynamic label
            Label lb_mCourse = new Label();
            lb_mCourse.Text = "Course";
            lb_mCourse.Location = new Point(360,320);
            lb_mCourse.Font = new Font(rtb_mDescription.Font.FontFamily, 14);

            //Create dynamic rich text box
            RichTextBox rtb_Course = new RichTextBox();
            for(int i =0; i< uminor.courses.Count; i++)
            {
                rtb_Course.Text += uminor.courses[i]+"\n";
            }
            rtb_Course.Location = new Point(240, 350);
            rtb_Course.Size = new Size(300, 180);
            rtb_Course.Font = new Font(rtb_Course.Font.FontFamily, 12);
            rtb_Course.ReadOnly = true;
            rtb_Course.BackColor = Color.Snow;
            
            //Create dynamic rich text box 
            RichTextBox rtb_note = new RichTextBox();
            rtb_note.Text = "Note: "+uminor.note;
            rtb_note.Location = new Point(100,540);
            rtb_note.Size = new Size(600, 120);
            rtb_note.Font = new Font(rtb_note.Font.FontFamily, 12);
            rtb_note.ReadOnly = true;



            mModal.Controls.Add(lb_min);
            mModal.Controls.Add(rtb_mDescription);
            mModal.Controls.Add(lb_mCourse);
            mModal.Controls.Add(rtb_Course);
            mModal.Controls.Add(rtb_note);
        }

        //The employment API
        public void employInfo()
        {
            // Get employment from the API
            string jsonEmp = rest.getRESTData("/employment/");

            // Convert the JSON to a C# object
            emp = JToken.Parse(jsonEmp).ToObject<Employment>();

            //Console.WriteLine(jsonEmp);
            lb_empTitle.Text = emp.introduction.title;

            
            for(int i=0; i< emp.introduction.content.Count; i++)
            {
                //create dynamic label
                Label lb_contentTitle = new Label();
                lb_contentTitle.Text = emp.introduction.content[i].title;
                lb_contentTitle.Location = new Point(150 + (400 * i), 80);
                lb_contentTitle.Size = new Size(300,50);
                lb_contentTitle.Font = new Font(lb_contentTitle.Font.FontFamily, 18);
                
                //create dynamic rich text box
                RichTextBox rtb_content = new RichTextBox();
                rtb_content.Text = emp.introduction.content[i].description;
                rtb_content.Location = new Point(50+(480*i),140);
                rtb_content.Size = new Size(400,220);
                rtb_content.BackColor = Color.Snow;
                rtb_content.ReadOnly = true;

                this.tp_emp.Controls.Add(lb_contentTitle);
                this.tp_emp.Controls.Add(rtb_content);
            }

            //Create dynamic label
            Label lb_statTitle = new Label();
            lb_statTitle.Text = emp.degreeStatistics.title;
            lb_statTitle.Location = new Point(360,380);
            lb_statTitle.Size = new Size(200,50);
            lb_statTitle.Font = new Font(lb_statTitle.Font.FontFamily, 18);

            this.tp_emp.Controls.Add(lb_statTitle);

            for (int i=0; i<emp.degreeStatistics.statistics.Count;i++) {
                //create the dynamic rich text box
                RichTextBox rtb_statistics = new RichTextBox();
                rtb_statistics.Text = "       "+emp.degreeStatistics.statistics[i].value+"\n"+ emp.degreeStatistics.statistics[i].description;
                rtb_statistics.Location = new Point(30 + (250 * i),440);
                rtb_statistics.Size = new Size(180,140);
                rtb_statistics.BackColor = Color.Snow;
                rtb_statistics.ReadOnly = true;
               
                this.tp_emp.Controls.Add(rtb_statistics);
            }


            lb_employer.Text = emp.employers.title;
            for(int i=0; i < emp.employers.employerNames.Count;i++)
            {
                tb_employer.Text += emp.employers.employerNames[i]+"\n";
            }

            lb_career.Text = emp.careers.title;
            for(int i=0; i < emp.careers.careerNames.Count; i++)
            {
                tb_career.Text += emp.careers.careerNames[i] + "\n";
            }

            bt_coopTable.Text = emp.coopTable.title;
            bt_empTable.Text = emp.employmentTable.title;

        }

        //display the coop information on pop up window
        private void bt_coopTable_Click(object sender, EventArgs e)
        {
            CoopModal coopM = new CoopModal();
            
            //Create dynamic list view to hold coop information
            ListView listCoop = new ListView();
            listCoop.View = View.Details;
            listCoop.Width = 560;          // set the width of the display
            listCoop.Height = 600;
            listCoop.Columns.Add("Degree", 120);
            listCoop.Columns.Add("Employe", 120);
            listCoop.Columns.Add("Location", 120);
            listCoop.Columns.Add("Term", 120);

            listCoop.Location = new Point(50,50);
            listCoop.Font = new Font(listCoop.Font.FontFamily, 15);

            ListViewItem item;
            for(var i=0; i< emp.coopTable.coopInformation.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    emp.coopTable.coopInformation[i].degree,
                    emp.coopTable.coopInformation[i].employer,
                    emp.coopTable.coopInformation[i].city,
                    emp.coopTable.coopInformation[i].term

                });

                listCoop.Items.Add(item);
                
            }

            coopM.Controls.Add(listCoop);
            coopM.ShowDialog();
        }

        //display the employer information on pop up window
        private void bt_empTable_Click(object sender, EventArgs e)
        {
            EmployModal empM = new EmployModal();

            //Create the dynamic list view
            ListView listEmp = new ListView();
            listEmp.View = View.Details;
            listEmp.Width = 640;          // set the width of the display
            listEmp.Height = 600;
            listEmp.Columns.Add("Degree", 120);
            listEmp.Columns.Add("Employe", 120);
            listEmp.Columns.Add("Location", 120);
            listEmp.Columns.Add("Title", 120);
            listEmp.Columns.Add("Term", 120);

            listEmp.Location = new Point(50, 50);
            listEmp.Font = new Font(listEmp.Font.FontFamily, 15);
            
            ListViewItem item;
            for (var i = 0; i < emp.employmentTable.professionalEmploymentInformation.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                   emp.employmentTable.professionalEmploymentInformation[i].employer,
                   emp.employmentTable.professionalEmploymentInformation[i].degree,
                   emp.employmentTable.professionalEmploymentInformation[i].city,
                   emp.employmentTable.professionalEmploymentInformation[i].title,
                   emp.employmentTable.professionalEmploymentInformation[i].startDate

                });

                listEmp.Items.Add(item);

            }
            
            empM.Controls.Add(listEmp);
            empM.ShowDialog();
        }

        //Show the map 
        private void ll_map_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://ist.rit.edu/api/map.php");
            ll_map.LinkVisited = true;
        }

        //The people API
        public void peopleInfo()
        {

            //Get people from the API
            string jsonPeople = rest.getRESTData("/people/");

            // Convert the JSON to a C# object
            people = JToken.Parse(jsonPeople).ToObject<People>();

            lb_peopleTitle.Text = people.title;
            lb_subTitle.Text = people.subTitle;

            for(int i=0;i< people.faculty.Count; i++)
            {
                //create dynamic buttons
                Button facButton = new Button();
                facButton.Name = people.faculty[i].username;
                facButton.Text = people.faculty[i].name;
                facButton.Size = new Size(160, 80);
                facButton.BackColor = Color.Snow;
                facButton.Cursor = Cursors.Hand;

                facButton.Margin = new Padding(15);

                facButton.Click += new EventHandler(gFacultyButtonClick);

                this.flp_faculty.Controls.Add(facButton);

            }

            for (int i = 0; i < people.staff.Count; i++)
            {
                //create dynamic buttons
                Button staButton = new Button();
                staButton.Name = people.staff[i].username;
                staButton.Text = people.staff[i].name;
                staButton.Size = new Size(160, 80);
                staButton.BackColor = Color.Snow;
                staButton.Cursor = Cursors.Hand;

                staButton.Margin = new Padding(15);

                staButton.Click += new EventHandler(gStaffButtonClick);

                this.flp_staff.Controls.Add(staButton);

            }

        }

        //Click the special faculty name button will show a modal window 
        public void gFacultyButtonClick(object sender, EventArgs e)
        {
            peopleM = new PeopleModal();
            string pName = null;
            pName = (sender as Button).Name;

            getPersonInfo(pName);

            peopleM.ShowDialog();
        }

        //Click the special staff name button will show a modal window
        public void gStaffButtonClick(object sender, EventArgs e)
        {
            peopleM = new PeopleModal();
            string sName = null;
            sName = (sender as Button).Name;

            getStaffInfo(sName);

            peopleM.ShowDialog();
        }
        
        //Display the special faculty information on modal window
        public void getPersonInfo(string peopName) {
            Faculty fac = people.faculty.Find(x => x.username == peopName);

            //create the label
            Label lb_fName = new Label();
            lb_fName.Text = fac.name+", "+fac.title;
            lb_fName.Location = new Point(130,30);
            lb_fName.Size = new Size(500,60);
            lb_fName.Font = new Font(lb_fName.Font.FontFamily, 20);

            //create the picture box
            PictureBox picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Size = new Size(260,260);
            picture.Location = new Point(300, 120);
            picture.Load(fac.imagePath);

            //create the label
            Label lb_fInfo = new Label();
            lb_fInfo.Text = fac.office+"\n" + fac.phone+ "\n"  + fac.email+"\n"+ fac.tagline + "\n" + fac.website+ "\n" + fac.twitter + "\n" + fac.facebook;
            lb_fInfo.Location = new Point(60, 120);
            lb_fInfo.Size = new Size(200,500);
            lb_fInfo.Font = new Font(lb_fName.Font.FontFamily, 13);

            // Console.WriteLine(fac.name);
            peopleM.Controls.Add(lb_fName);
            peopleM.Controls.Add(picture);
            peopleM.Controls.Add(lb_fInfo);

        }

        //Display the special staff information on modal window
        public void getStaffInfo(string staName)
        {
            Staff sta = people.staff.Find(x => x.username == staName);
            //create the label
            Label lb_sName = new Label();
            lb_sName.Text = sta.name + ", " + sta.title;
            lb_sName.Location = new Point(130, 30);
            lb_sName.Size = new Size(500, 60);
            lb_sName.Font = new Font(lb_sName.Font.FontFamily, 20);

            //create the picture box 
            PictureBox picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Size = new Size(260, 260);
            picture.Location = new Point(300, 120);
            picture.Load(sta.imagePath);

            //create the label
            Label lb_sInfo = new Label();
            lb_sInfo.Text = sta.office + "\n" + sta.phone + "\n" + sta.email + "\n" + sta.tagline + "\n" + sta.website + "\n" + sta.twitter + "\n" + sta.facebook;
            lb_sInfo.Location = new Point(60, 120);
            lb_sInfo.Size = new Size(200, 500);
            lb_sInfo.Font = new Font(lb_sName.Font.FontFamily, 13);

       
            peopleM.Controls.Add(lb_sName);
            peopleM.Controls.Add(picture);
            peopleM.Controls.Add(lb_sInfo);
        }

        //The research API
        public void researchInfo()
        {
            //Get research from the API
            string jsonResearch = rest.getRESTData("/research/");

            // Convert the JSON to a C# object
            research = JToken.Parse(jsonResearch).ToObject<Research>();

            

            for(int i =0; i < research.byInterestArea.Count; i++)
            {
                //create the button
                Button areaButton = new Button();
                areaButton.Name = research.byInterestArea[i].areaName;
                areaButton.Text = research.byInterestArea[i].areaName;
                areaButton.Size = new Size(160, 80);
                areaButton.BackColor = Color.Snow;
                areaButton.Cursor = Cursors.Hand;

                areaButton.Margin = new Padding(15);
                areaButton.Click += new EventHandler(gAreaButtonClick);

                this.flp_area.Controls.Add(areaButton);
            }


            for (int i =0; i<research.byFaculty.Count;i++)
            {
                string path = null;
                //create the picture box 
                PictureBox ptb_Faculty = new PictureBox();
                ptb_Faculty.Name = research.byFaculty[i].username;
                ptb_Faculty.SizeMode = PictureBoxSizeMode.StretchImage;
                ptb_Faculty.Size = new Size(160,160);
                ptb_Faculty.Cursor = Cursors.Hand;
                ptb_Faculty.Margin = new Padding(15);
                path = "https://ist.rit.edu/assets/img/people/" + research.byFaculty[i].username+".jpg";
                ptb_Faculty.Load(path);

                ptb_Faculty.Click += new EventHandler(gLookupImageClick);

                flp_lookup.Controls.Add(ptb_Faculty);
            }
            
        }

        //Click the special research area name button will show a modal window
        public void gAreaButtonClick(object sender, EventArgs e)
        {
            mModal = new MinorModal();
            string rName = null;
            rName = (sender as Button).Name;

            getAreaInfo(rName);

            mModal.ShowDialog();
        }

        //Display the special research area name information on modal window
        public void getAreaInfo(string resName)
        {
            ByInterestArea bic = research.byInterestArea.Find(x => x.areaName == resName);
            //create the label
            Label lb_areaName = new Label();
            lb_areaName.Text = bic.areaName;
            lb_areaName.Location = new Point(360,40);
            lb_areaName.Size = new Size(400, 80);
            lb_areaName.Font = new Font(lb_areaName.Font.FontFamily, 20);

            //create the rich text box
            RichTextBox rtb_Area = new RichTextBox();
            rtb_Area.Location = new Point(80,130);
            rtb_Area.Size = new Size(670, 500);
            rtb_Area.Font = new Font(rtb_Area.Font.FontFamily, 16);
            rtb_Area.BackColor = Color.Snow;

            for(int i =0; i< bic.citations.Count; i++)
            {
                rtb_Area.Text += "--- "+bic.citations[i]+"\n";
                mModal.Controls.Add(rtb_Area);
            }

            mModal.Controls.Add(lb_areaName);

         //   Console.WriteLine(bic.areaName);
        }

        //Click the special faculty image will show a modal window
        public void gLookupImageClick(object sender, EventArgs e)
        {
            mModal = new MinorModal();
            string lName = null;
            lName = (sender as  PictureBox).Name;

            getLookUpInfo(lName);

            mModal.ShowDialog();
        }

        //Display the special faculty information on modal window
        public void getLookUpInfo(string loopName)
        {
            ByFaculty bf = research.byFaculty.Find(x => x.username == loopName);
            //create the label
            Label lb_loopName = new Label();
            lb_loopName.Text = bf.facultyName;
            lb_loopName.Location = new Point(320, 40);
            lb_loopName.Size = new Size(400, 80);
            lb_loopName.Font = new Font(lb_loopName.Font.FontFamily, 20);

            //create the rich text box 
            RichTextBox rtb_Loop = new RichTextBox();
            rtb_Loop.Location = new Point(80, 130);
            rtb_Loop.Size = new Size(670, 500);
            rtb_Loop.Font = new Font(rtb_Loop.Font.FontFamily, 16);
            rtb_Loop.ReadOnly = true;
            rtb_Loop.BackColor = Color.Snow;
            for (int i = 0; i < bf.citations.Count; i++)
            {
                rtb_Loop.Text += "--- " + bf.citations[i] + "\n";
                mModal.Controls.Add(rtb_Loop);
            }

            mModal.Controls.Add(lb_loopName);
        }

        //The resource API
        public void resourceInfo()
        {
            // Get Resource from the API
            string jsonResource = rest.getRESTData("/resources/");

            // Convert the JSON to a C# object
            resou = JToken.Parse(jsonResource).ToObject<Resources>();

            lb_resource.Text = resou.title;
            lb_resouSubTitle.Text = resou.subTitle;

            bt_sa.Text = resou.studyAbroad.title;
            bt_ss.Text = resou.studentServices.title;
            bt_tali.Text = resou.tutorsAndLabInformation.title;
            bt_sam.Text = resou.studentAmbassadors.title;
            bt_forms.Text = "Forms";
            bt_ce.Text = resou.coopEnrollment.title;
        }

        //Display the study abroad information
        private void bt_sa_Click(object sender, EventArgs e)
        {
            mModal = new MinorModal();
            Label lb_studyName = new Label();
            lb_studyName.Text = resou.studyAbroad.title;
            lb_studyName.Location = new Point(320, 40);
            lb_studyName.Size = new Size(400, 60);
            lb_studyName.Font = new Font(lb_studyName.Font.FontFamily, 20);

            RichTextBox rtb_studyDes = new RichTextBox();
            rtb_studyDes.Text = resou.studyAbroad.description;
            rtb_studyDes.Location = new Point(80, 110);
            rtb_studyDes.Size = new Size(670, 180);
            rtb_studyDes.Font = new Font(rtb_studyDes.Font.FontFamily, 15);
            rtb_studyDes.BackColor = Color.Snow;

            for(int i = 0; i < resou.studyAbroad.places.Count; i++)
            {
                Label lb_placeName = new Label();
                lb_placeName.Text = resou.studyAbroad.places[i].nameOfPlace;
                lb_placeName.Location = new Point(80,320+(300*i));
                lb_placeName.Size = new Size(400,50);
                lb_placeName.Font = new Font(lb_placeName.Font.FontFamily, 18);

                mModal.Controls.Add(lb_placeName);

                RichTextBox rtb_pDesc = new RichTextBox();
                rtb_pDesc.Text = resou.studyAbroad.places[i].description;
                rtb_pDesc.Location = new Point(80, 400+(300*i));
                rtb_pDesc.Size = new Size(670, 180);
                rtb_pDesc.ReadOnly = true;
                rtb_pDesc.BackColor = Color.Snow;
                rtb_pDesc.Font = new Font(rtb_pDesc.Font.FontFamily, 15);

                mModal.Controls.Add(rtb_pDesc);

            }

            mModal.Controls.Add(lb_studyName);
            mModal.Controls.Add(rtb_studyDes);

            mModal.ShowDialog();
        }


        //Display the student services information 
        private void bt_ss_Click(object sender, EventArgs e)
        {
            mModal = new MinorModal();

            Label lb_ssTitle = new Label();
            lb_ssTitle.Text = resou.studentServices.title;
            lb_ssTitle.Location = new Point(320, 40);
            lb_ssTitle.Size = new Size(400, 60);
            lb_ssTitle.Font = new Font(lb_ssTitle.Font.FontFamily, 20);

            mModal.Controls.Add(lb_ssTitle);


            Label lb_aa = new Label();
            lb_aa.Text = resou.studentServices.academicAdvisors.title;
            lb_aa.Location = new Point(80,80);
            lb_aa.Size = new Size(400,60);
            lb_aa.Font = new Font(lb_aa.Font.FontFamily, 17);

            mModal.Controls.Add(lb_aa);

            RichTextBox rtb_aaDesc = new RichTextBox();
            rtb_aaDesc.Text = resou.studentServices.academicAdvisors.description;
            rtb_aaDesc.Location = new Point(80, 140);
            rtb_aaDesc.Size = new Size(670,180);
            rtb_aaDesc.ReadOnly = true;
            rtb_aaDesc.BackColor = Color.Snow;
            rtb_aaDesc.Font = new Font(lb_aa.Font.FontFamily, 15);

            mModal.Controls.Add(rtb_aaDesc);

            Label lb_faq = new Label();
            lb_faq.Text = resou.studentServices.academicAdvisors.faq.title;
            lb_faq.Location = new Point(80,340);
            lb_faq.Size = new Size(400,60);
            lb_faq.Font = new Font(lb_faq.Font.FontFamily, 17);
            mModal.Controls.Add(lb_faq);

            LinkLabel ll_content = new LinkLabel();
            ll_content.Text = resou.studentServices.academicAdvisors.faq.contentHref;
            ll_content.Location = new Point(80,380);
            ll_content.Size = new Size(700, 60);
            ll_content.Font = new Font(lb_faq.Font.FontFamily, 14);
            mModal.Controls.Add(ll_content);



            Label lb_pa = new Label();
            lb_pa.Text = resou.studentServices.professonalAdvisors.title;
            lb_pa.Location = new Point(80, 450);
            lb_pa.Size = new Size(400, 60);
            lb_pa.Font = new Font(lb_pa.Font.FontFamily, 17);
            mModal.Controls.Add(lb_pa);


            RichTextBox rtb_ai = new RichTextBox();
            rtb_ai.Location = new Point(80, 515);
            rtb_ai.Size = new Size(670, 180);
            rtb_ai.ReadOnly = true;
            rtb_ai.BackColor = Color.Snow;
            rtb_ai.Font = new Font(rtb_ai.Font.FontFamily, 15);

            for (int i = 0; i < resou.studentServices.professonalAdvisors.advisorInformation.Count; i++)
            {
                rtb_ai.Text += resou.studentServices.professonalAdvisors.advisorInformation[i].name+"\n--"+ resou.studentServices.professonalAdvisors.advisorInformation[i].department+"\n--"+ resou.studentServices.professonalAdvisors.advisorInformation[i].email+"\n";
            }

            mModal.Controls.Add(rtb_ai);

            Label lb_fa = new Label();
            lb_fa.Text = resou.studentServices.facultyAdvisors.title;
            lb_fa.Location = new Point(80, 710);
            lb_fa.Size = new Size(400, 60);
            lb_fa.Font = new Font(lb_fa.Font.FontFamily, 17);
            mModal.Controls.Add(lb_fa);


            RichTextBox rtb_faDesc = new RichTextBox();
            rtb_faDesc.Text = resou.studentServices.facultyAdvisors.description;
            rtb_faDesc.Location = new Point(80, 775);
            rtb_faDesc.Size = new Size(670, 180);
            rtb_faDesc.ReadOnly = true;
            rtb_faDesc.BackColor = Color.Snow;
            rtb_faDesc.Font = new Font(rtb_faDesc.Font.FontFamily, 15);
            mModal.Controls.Add(rtb_faDesc);

            Label lb_ima = new Label();
            lb_ima.Text = resou.studentServices.istMinorAdvising.title;
            lb_ima.Location = new Point(80,960);
            lb_ima.Size = new Size(400, 60);
            lb_ima.Font = new Font(lb_ima.Font.FontFamily, 17);
            mModal.Controls.Add(lb_ima);


            RichTextBox rtb_madi = new RichTextBox();
            rtb_madi.Location = new Point(80, 1020);
            rtb_madi.Size = new Size(670, 180);
            rtb_madi.ReadOnly = true;
            rtb_madi.BackColor = Color.Snow;
            rtb_madi.Font = new Font(rtb_madi.Font.FontFamily, 15);

            for (int i = 0;i < resou.studentServices.istMinorAdvising.minorAdvisorInformation.Count;i++)
            {
                rtb_madi.Text += resou.studentServices.istMinorAdvising.minorAdvisorInformation[i].title+ "\n--" + resou.studentServices.istMinorAdvising.minorAdvisorInformation[i].advisor + "\n--" + resou.studentServices.istMinorAdvising.minorAdvisorInformation[i].email + "\n";
            }

            mModal.Controls.Add(rtb_madi);

            mModal.ShowDialog();
        }


        //Display the tutors and lab information
        private void bt_tali_Click(object sender, EventArgs e)
        {
            mModal = new MinorModal();

            Label lb_taliName = new Label();
            lb_taliName.Text = resou.studyAbroad.title;
            lb_taliName.Location = new Point(320, 40);
            lb_taliName.Size = new Size(600, 60);
            lb_taliName.Font = new Font(lb_taliName.Font.FontFamily, 20);

            mModal.Controls.Add(lb_taliName);

            RichTextBox rtb_taliDes = new RichTextBox();
            rtb_taliDes.Text = resou.studyAbroad.description;
            rtb_taliDes.Location = new Point(80, 110);
            rtb_taliDes.Size = new Size(670, 180);
            rtb_taliDes.ReadOnly = true;
            rtb_taliDes.Font = new Font(rtb_taliDes.Font.FontFamily, 15);
            rtb_taliDes.BackColor = Color.Snow;

            mModal.Controls.Add(rtb_taliDes);

            LinkLabel ll_tutor = new LinkLabel();
            ll_tutor.Text = resou.tutorsAndLabInformation.tutoringLabHoursLink;
            ll_tutor.Location = new Point(80, 300);
            ll_tutor.Size = new Size(700, 60);
            ll_tutor.Font = new Font(ll_tutor.Font.FontFamily, 14);
            mModal.Controls.Add(ll_tutor);



            mModal.ShowDialog();

        }

        //Display the student ambassadors information
        private void bt_sam_Click(object sender, EventArgs e)
        {
            mModal = new MinorModal();

            Label lb_stamName = new Label();
            lb_stamName.Text = resou.studentAmbassadors.title;
            lb_stamName.Location = new Point(140, 40);
            lb_stamName.Size = new Size(600, 60);
            lb_stamName.Font = new Font(lb_stamName.Font.FontFamily, 20);

            mModal.Controls.Add(lb_stamName);


            PictureBox pb = new PictureBox();
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Size = new Size(460, 260);
            pb.Location = new Point(160,120);
            pb.Load(resou.studentAmbassadors.ambassadorsImageSource);

            mModal.Controls.Add(pb);

            RichTextBox rtb_ssc = new RichTextBox();
            rtb_ssc.Location = new Point(80, 400);
            rtb_ssc.Size = new Size(670, 280);
            rtb_ssc.Font = new Font(rtb_ssc.Font.FontFamily, 15);
            rtb_ssc.BackColor = Color.Snow;

            for(int i=0;i< resou.studentAmbassadors.subSectionContent.Count; i++)
            {
                rtb_ssc.Text += "---"+resou.studentAmbassadors.subSectionContent[i].title + "\n" + resou.studentAmbassadors.subSectionContent[i].description + "\n";
            }

            mModal.Controls.Add(rtb_ssc);

            LinkLabel ll_afl = new LinkLabel();
            ll_afl.Text = resou.studentAmbassadors.applicationFormLink;
            ll_afl.Location = new Point(80, 700);
            ll_afl.Size = new Size(700, 60);
            ll_afl.Font = new Font(ll_afl.Font.FontFamily, 16);
            mModal.Controls.Add(ll_afl);


            RichTextBox rtb_no = new RichTextBox();
            rtb_no.Text = resou.studentAmbassadors.note;
            rtb_no.Location = new Point(80, 780);
            rtb_no.Size = new Size(670, 100);
            rtb_no.Font = new Font(rtb_no.Font.FontFamily, 15);
            rtb_no.BackColor = Color.Snow;
            mModal.Controls.Add(rtb_no);


            mModal.ShowDialog();
        }

        //Display the form
        private void bt_forms_Click(object sender, EventArgs e)
        {
            mModal = new MinorModal();

            Label lb_taliName = new Label();
            lb_taliName.Text = "Graduate Forms";
            lb_taliName.Location = new Point(320, 40);
            lb_taliName.Size = new Size(600, 60);
            lb_taliName.Font = new Font(lb_taliName.Font.FontFamily, 20);

            mModal.Controls.Add(lb_taliName);

            for (int i =0; i < resou.forms.graduateForms.Count; i++)
            {
                RichTextBox rtb_gf = new RichTextBox();
                rtb_gf.Location = new Point(80, 110+(130*i));
                rtb_gf.Size = new Size(670, 100);
                rtb_gf.Font = new Font(rtb_gf.Font.FontFamily, 15);
                rtb_gf.ReadOnly = true;
                rtb_gf.BackColor = Color.Snow;
                rtb_gf.Text = "---"+resou.forms.graduateForms[i].formName + "\n" + resou.forms.graduateForms[i].href + "\n";
                mModal.Controls.Add(rtb_gf);
            }

            Label lb_underName = new Label();
            lb_underName.Text = "Undergraduate Forms";
            lb_underName.Location = new Point(320, 1040);
            lb_underName.Size = new Size(600, 60);
            lb_underName.Font = new Font(lb_underName.Font.FontFamily, 20);

            mModal.Controls.Add(lb_underName);


            for (int i = 0; i < resou.forms.undergraduateForms.Count; i++)
            {
                RichTextBox rtb_uf = new RichTextBox();
                rtb_uf.Location = new Point(80, 1100 + (130 * i));
                rtb_uf.Size = new Size(670, 100);
                rtb_uf.Font = new Font(rtb_uf.Font.FontFamily, 15);
                rtb_uf.ReadOnly = true;
                rtb_uf.BackColor = Color.Snow;
                rtb_uf.Text = "---" + resou.forms.undergraduateForms[i].formName + "\n" + resou.forms.undergraduateForms[i].href + "\n";
                mModal.Controls.Add(rtb_uf);
            }

            mModal.ShowDialog();
        }

        //Display the coop enrollment information
        private void bt_ce_Click(object sender, EventArgs e)
        {
            mModal = new MinorModal();

            Label lb_ceName = new Label();
            lb_ceName.Text = resou.coopEnrollment.title;
            lb_ceName.Location = new Point(320, 40);
            lb_ceName.Size = new Size(600, 60);
            lb_ceName.Font = new Font(lb_ceName.Font.FontFamily, 20);

            mModal.Controls.Add(lb_ceName);


            for (int i = 0; i < resou.coopEnrollment.enrollmentInformationContent.Count; i++)
            {
                RichTextBox rtb_uf = new RichTextBox();
                rtb_uf.Location = new Point(80, 120 + (250 * i));
                rtb_uf.Size = new Size(670, 200);
                rtb_uf.Font = new Font(rtb_uf.Font.FontFamily, 15);
                rtb_uf.ReadOnly = true;
                rtb_uf.BackColor = Color.Snow;
                rtb_uf.Text = "---" + resou.coopEnrollment.enrollmentInformationContent[i].title + "\n" + resou.coopEnrollment.enrollmentInformationContent[i].description + "\n";
                mModal.Controls.Add(rtb_uf);
            }

            LinkLabel ll_rit = new LinkLabel();
            ll_rit.Text = resou.coopEnrollment.RITJobZoneGuidelink;
            ll_rit.Location = new Point(80, 1250);
            ll_rit.Size = new Size(700, 60);
            ll_rit.Font = new Font(ll_rit.Font.FontFamily, 16);
            mModal.Controls.Add(ll_rit);


            mModal.ShowDialog();
        }

        //The news APIT
        public void newsInfo()
        {
            //Get News from the API
            string jsonNews = rest.getRESTData("/news/");

            // Convert the JSON to a C# object
            news = JToken.Parse(jsonNews).ToObject<News>();

            for (int i = 0; i < news.older.Count; i++)
            {
                Button nsDegree = new Button();
                nsDegree.Name = news.older[i].title;
                nsDegree.Text = news.older[i].title + "        "+ news.older[i].date;
                nsDegree.Size = new Size(400, 100);
                nsDegree.BackColor = Color.Snow;
                nsDegree.Cursor = Cursors.Hand;

                nsDegree.Click += new EventHandler(newsButtonClick);

                this.flp_news.Controls.Add(nsDegree);
            }


        }

        //Click the special news and display the pop up window
        public void newsButtonClick(object sender, EventArgs e)
        {
            mModal = new MinorModal();

            string nName = null;
            nName = (sender as Button).Name;

            getSingleNews(nName);

            //    Console.WriteLine(dName);

            mModal.ShowDialog();

        }

        //Display the special news on the modal window
        public void getSingleNews(string newName)
        {
            Older older = news.older.Find(x => x.title == newName);

            Console.WriteLine(older.title);

            //create the label to hold the news name
            Label lb_olderName = new Label();
            lb_olderName.Text = older.title;
            lb_olderName.Location = new Point(40, 40);
            lb_olderName.Size = new Size(800, 60);
            lb_olderName.Font = new Font(lb_olderName.Font.FontFamily, 18);

            //create the rich text box to display descrip
            RichTextBox rtb_oldDescr = new RichTextBox();
            rtb_oldDescr.Location = new Point(80, 115);
            rtb_oldDescr.Size = new Size(670, 500);
            rtb_oldDescr.ReadOnly = true;
            rtb_oldDescr.BackColor = Color.Snow;
            rtb_oldDescr.Font = new Font(rtb_oldDescr.Font.FontFamily, 13);
            rtb_oldDescr.Text = older.description;
            mModal.Controls.Add(rtb_oldDescr);

            mModal.Controls.Add(lb_olderName);
        }

        //The footer API
        public void footInfo()
        {
            // Get Foot from the API
            string jsonFoot = rest.getRESTData("/footer/");

            // Convert the JSON to a C# object
            foot = JToken.Parse(jsonFoot).ToObject<Foot>();

            lb_footTitle.Text = foot.social.title;
            lb_tweet.Text = foot.social.tweet;
            lb_by.Text = foot.social.by;
            lb_twitter.Text = foot.social.twitter;
            lb_facebook.Text = foot.social.facebook;

            for(int i=0; i < foot.quickLinks.Count; i++)
            {
                //craete the label
                Label lb = new Label();
                lb.Text = foot.quickLinks[i].title;
                lb.Location = new Point(120,250+(120*i));
                lb.Size = new Size(600,50);
                tp_aboutus.Controls.Add(lb);

                //create the link label
                LinkLabel ll_quick = new LinkLabel();
                ll_quick.Name = foot.quickLinks[i].href;
                ll_quick.Text = foot.quickLinks[i].href;
                ll_quick.Size = new Size(600,50);
                ll_quick.Location = new Point(120, 310+(120*i));
                ll_quick.LinkClicked += new LinkLabelLinkClickedEventHandler(glinkClick);

                tp_aboutus.Controls.Add(ll_quick);
            }

            Label lb_c = new Label();
            lb_c.Text = foot.copyright.title +"     "+ foot.copyright.html;
            lb_c.Location = new Point(120,790);
            lb_c.Size = new Size(800,80);

            tp_aboutus.Controls.Add(lb_c);     
        }

        //Click the special linke
        public void glinkClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string lName = null;
            lName = (sender as LinkLabel).Name;

            getLinkInfo(lName);
        }

        //Jump to the link when click
        public void getLinkInfo(String linName)
        {
            QuickLink ql = foot.quickLinks.Find(x => x.href == linName);
            System.Diagnostics.Process.Start(ql.href);
           

        }
    }



}

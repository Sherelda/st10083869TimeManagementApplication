using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeManagementLib;

//This is the application for the time management 
namespace st10083869TimeManagementApplication
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Initializes a new Semester with default values
            Semester = new Semester
            {
                Modules = new List<Module>(),
                NumberOfWeeks = 0,
                StartDate = DateTime.Today
            };
        }

        //Access the semster 

        public Semester Semester { get; }

        // Event handler to add  modules
        private void btnAddModule_Click(object sender, RoutedEventArgs e, Semester semester)
        {
            string code = txtCode.Text;
            string name = txtName.Text;
            int credits;
            int classHours;

            
            if (!int.TryParse(txtCredits.Text, out credits) || !int.TryParse(txtClassHours.Text, out classHours))
            {
                MessageBox.Show("Please enter valid numeric values for Credits.");
                return;
            }

            //creates a new module
            Module module = new Module
            {
                Code = code,
                Name = name,
                Credits = credits,
                ClassHoursPerWeek = classHours
            };

            //Access list for modules
            List<Module> modules = semester.Modules;
            modules.Add(module);
            lbModules.Items.Add(module.Code);
        }

        //used to record hours 
        private void btnRecordHours_Click(object sender, RoutedEventArgs e, Semester semester)
        {
            int selectedIndex = lbModules.SelectedIndex;

            if (selectedIndex >= 0)
            {
                //retrives the modules 
                Module selectedModule = semester.Modules[selectedIndex];
                double hours;

                // Input validation
                if (!double.TryParse(txtHours.Text, out hours))
                {
                    MessageBox.Show(" Enter Number of hours in Class.");
                    return;
                }

                // Assuming you have a separate class to represent recorded hours
                // You will need to create a class named "RecordedHours" with a "Date" and "Hours" property

                var recordedHours = new RecordedHours { Date = DateTime.Now, Hours = hours };
                selectedModule.WeeklyHours.Add(recordedHours);

                double remainingHours = semester.CalculateRemainingSelfStudyHours(selectedModule);
                MessageBox.Show($"Remaining self-study hours for {selectedModule.Name}: {remainingHours}");
            }
            else
            {
                MessageBox.Show("Please select a module from the list.");
            }
        }
    }
}
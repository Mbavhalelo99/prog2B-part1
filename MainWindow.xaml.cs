// MainWindow.xaml.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Part_1;

namespace Part_1
{
    public partial class MainWindow : Window
    {
        private List<Module> modules = new List<Module>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            // Create a new module and add it to the list
            Module newModule = new(Module)
            {
                Code = ModuleCode.Text,
                Name = ModuleName.Text,
                Credits = int.Parse(ModuleCredits.Text),
                ClassHoursPerWeek = int.Parse(ModuleClassHours.Text)
            };

            modules.Add(newModule);

            // Clear input fields
            ModuleCode.Clear();
            ModuleName.Clear();
            ModuleCredits.Clear();
            ModuleClassHours.Clear();

            UpdateModuleList();
        }

        private void UpdateModuleList()
        {
            ModulesListView.ItemsSource = modules;
        }

        private void CalculateSelfStudy_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(WeeksInSemester.Text, out int weeks))
            {
                foreach (var module in modules)
                {
                    int remainingSelfStudyHours = module.CalculateRemainingSelfStudyHours(weeks);
                    module.SelfStudyHoursThisWeek = remainingSelfStudyHours;
                }

                UpdateModuleList();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of weeks.");
            }
        }

        private void RecordStudyHours_Click(object sender, RoutedEventArgs e)
        {
            if (ModulesListView.SelectedItem is Module selectedModule &&
                int.TryParse(HoursWorked.Text, out int hours))
            {
                selectedModule.StudyRecords.Add(new StudyRecord
                {
                    Date = DateTime.Now,
                    Hours = hours
                });

                UpdateModuleList();
            }
            else
            {
                MessageBox.Show("Please select a module and enter valid hours worked.");
            }
        }
    }
}


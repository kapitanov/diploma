using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AISTek.DataFlow.PresentationExtensions;
using AISTek.DataFlow.Shared.Classes;
using AISTek.DataFlow.Shared.Client;
using AISTek.DataFlow.Tools.JobEditor.DataModel;
using AISTek.DataFlow.Tools.JobEditor.DataModel.Stages;

namespace AISTek.DataFlow.Tools.JobEditor.Views
{
    /// <summary>
    /// Interaction logic for UploadJobView.xaml
    /// </summary>
    public partial class UploadJobView
    {
        public UploadJobView(StagesProvider stagesProvider, JobDefinition job, IRepositoryClientFactory repositoryClientFactory, IJobManager jobManager)
        {
            InitializeComponent();
            this.stagesProvider = stagesProvider;
            this.jobManager = jobManager;
            this.repositoryClientFactory = repositoryClientFactory;
            this.job = job;
            listStages.ItemsSource = stagesProvider.Stages;
            progressBar.Maximum = ProgressBarMaximum;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.RemoveIcon();
            this.HideCloseButton();
        }

        private readonly StagesProvider stagesProvider;
        private readonly JobDefinition job;
        private readonly IRepositoryClientFactory repositoryClientFactory;
        private readonly IJobManager jobManager;
        private const int ProgressBarMaximum = 100;

        private void AeroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveProgress();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using (var repository = repositoryClientFactory.CreateClient())
                {
                    var currentStage = 0;
                    var totalStages = stagesProvider.Stages.Count + 1;

                    Action<int, int> setProgress = (current, total) => 
                        this.Synch(w =>
                            SetProgress(current, total, currentStage, totalStages));

                    Action<string> setStatus = status => this.Synch(w => w.textStatus.Text = status);

                    foreach (var stage in stagesProvider.Stages)
                    {
                        stage.Owner = this;
                        stage.State = StageState.Processing;
                        this.Synch(w =>
                        {
                            w.listStages.ItemsSource = stagesProvider.Stages;
                            w.listStages.UpdateLayout();
                        });
                        try
                        {
                            setProgress(1, 1);
                            stage.Execute(job, setProgress, setStatus, repository, jobManager);
                            stage.State = StageState.Completed;
                            currentStage++;
                        }
                        catch (StageFailedException error)
                        {
                            stage.State = StageState.Failed;
                            this.Synch(w =>
                            {
                                SetFailureProgress();
                                SetFullProgress();
                                w.panelClose.Visibility = Visibility.Visible;
                            });
                            setStatus(error.Message);
                            return;
                        }
                        catch (Exception error)
                        {
                            stage.State = StageState.Failed;
                            this.Synch(w =>
                            {
                                SetFailureProgress();
                                SetFullProgress();
                                w.panelClose.Visibility = Visibility.Visible;
                            });
                            setStatus(error.Message);
                            MessageBox.Show(error.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    setStatus("Done!");
                    this.Synch(w =>
                    {
                        SetFullProgress();
                        w.panelClose.Visibility = Visibility.Visible;
                    });
                }
            });
        }

        private void SetProgress(
            int progressInStage, 
            int maxProgressInStage, 
            int currentStage, 
            int totalStages)
        {
            progressBar.Maximum = ProgressBarMaximum * totalStages;
            progressBar.Value = currentStage * ProgressBarMaximum +
                (int)(ProgressBarMaximum * progressInStage / (float)maxProgressInStage);          
        }

        private void SetFullProgress()
        {
            progressBar.Maximum = ProgressBarMaximum;
            progressBar.Value = ProgressBarMaximum;
        }

        private void SetActiveProgress()
        {
            progressBar.Foreground = Resources["AeroProgressBarActive"] as Brush;
        }

        private void SetFailureProgress()
        {
            progressBar.Foreground = Resources["AeroProgressBarError"] as Brush;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

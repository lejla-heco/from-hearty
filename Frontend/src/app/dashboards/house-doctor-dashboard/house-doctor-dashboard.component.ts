import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { Chart, ChartConfiguration, registerables, ChartType } from 'chart.js/auto';
import { HttpClient } from '@angular/common/http';
import { AiPredictionService } from '../../home-doctor/ai-prediction/ai-prediction.service';
import { Config } from '../../configuration/config';
import { monthLabels } from '../dashboards-config';
import { AuthentificationHelper } from '../../authentification/authentification-helper';
import { AppointmentService } from '../../appointment/appointment.service';
Chart.register(...registerables);

@Component({
  selector: 'app-house-doctor-dashboard',
  templateUrl: './house-doctor-dashboard.component.html',
  styleUrl: './house-doctor-dashboard.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class HouseDoctorDashboardComponent implements OnInit {
  public predictionsPerMonthBarChart!: Chart;
  public predictionsPerMonthDataset: any[] = [];

  public appointmentsPerMonthLineChart!: Chart;
  public approvedAppointmentsDataset: any[] = [];
  public pendingAppointmentsDataset: any[] = [];

  public doughnutPercentagesChart!: any;
  public doughnutChartData: any[] = [];

  public urgentPatientsBarChart!: any;
  public urgentPatientsData: any = {};

  public userId = AuthentificationHelper.getLoginToken().userId;

  constructor(private httpClient: HttpClient, private aiPredictionService: AiPredictionService, private appointmentService: AppointmentService) {

  }

  ngOnInit() {
    this.getPredictionsPerMonth();
    this.getApprovedAppointmentsPerMonth();
    this.getPredictionPercentages();
    this.getUrgentPatients();
  }

  getPredictionsPerMonth(): void {
    this.httpClient.get(Config.serverAddress + this.aiPredictionService.api.predictionsPerMonth + '/' + this.userId).subscribe((response: any) => {
      this.predictionsPerMonthDataset = response.map((item: any) => ({
        ...item,
        borderWidth: 2
      }));
      this.initPredictionsPerMonthBarChart();
    });
  }

  getApprovedAppointmentsPerMonth(): void {
    this.httpClient.get(Config.serverAddress + this.appointmentService.api.approvedAppointments).subscribe((response: any) => {
      this.approvedAppointmentsDataset = response;
      this.getPendingAppointmentsPerMonth();
    });
  }

  getPendingAppointmentsPerMonth(): void {
    this.httpClient.get(Config.serverAddress + this.appointmentService.api.pendingAppointments).subscribe((response: any) => {
      this.pendingAppointmentsDataset = response;
      this.initAppointmentsLineChart();
    });
  }

  getPredictionPercentages(): void {
    this.httpClient.get(Config.serverAddress + this.aiPredictionService.api.predictionResultPercentages + '/' + this.userId).subscribe((response: any) => {
      this.doughnutChartData = response;
      this.initDoughnutChart();
    });
  }

  getUrgentPatients(): void {
    this.httpClient.get(Config.serverAddress + this.aiPredictionService.api.urgentPatients).subscribe((response: any) => {
      this.urgentPatientsData = response;
      this.initUrgentPatientsBarChart();
    });
  }

  initDoughnutChart(): void {
    this.doughnutPercentagesChart = new Chart("canvas-doughnut", {
      type: 'doughnut',
      data: {
        labels: ['0-15%', '16-55%', '56+'],
        datasets: [
          {
            data: this.doughnutChartData,
            backgroundColor: [
              'rgba(102, 204, 102, 0.2)',
              'rgba(255, 159, 64, 0.2)',
              'rgba(255, 99, 132, 0.2)',
            ],
            borderColor: [
              'rgba(102, 204, 102)',
              'rgb(255, 159, 64)',
              'rgb(255, 99, 132)',
            ],
            borderWidth: 1
          }
        ]
      },
      options: {
        cutout: '60%',
        aspectRatio: 2,
        plugins: {
          legend: {
            display: true,
            position: 'right',
          },
        },
      },
    });
  }

  initPredictionsPerMonthBarChart(): void {
    this.predictionsPerMonthBarChart = new Chart("canvas-predictions-per-month", {
      type: 'bar',
      data: {
        labels: [''],
        datasets: this.predictionsPerMonthDataset
      },
      options: {
        scales: {
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Number of created predictions'
            },
          },
        },
        plugins: {
          legend: {
            display: true,
            position: 'right'
          }
        },
      },
    });
  }

  initAppointmentsLineChart(): void {
    this.appointmentsPerMonthLineChart = new Chart("canvas-appointment-status-per-month", {
      type: 'line',
      data: {
        labels: monthLabels,
        datasets: [
          {
            label: 'Approved appointments',
            data: this.approvedAppointmentsDataset,
            fill: false,
            cubicInterpolationMode: 'monotone',
            tension: 0.4
          }, {
            label: 'Pending appointments',
            data: this.pendingAppointmentsDataset,
            fill: false,
            tension: 0.4
          }
        ]
      },
      options: {
        responsive: true,
        interaction: {
          intersect: false,
        },
        scales: {
          x: {
            display: true,
            title: {
              display: true
            }
          },
          y: {
            display: true,
            title: {
              display: true,
              text: 'Value'
            },
            suggestedMin: 0,
            suggestedMax: 200
          }
        }
      },
    });
  }

  initUrgentPatientsBarChart(): void {
    this.urgentPatientsBarChart = new Chart("canvas-urgent-patients", {
      type: 'bar',
      data: {
        labels: this.urgentPatientsData.labels,
        datasets: [
          {
            label: 'Probability percentage',
            data: this.urgentPatientsData.data,
          },
        ]
      },
      options: {
        indexAxis: 'y',
        elements: {
          bar: {
            borderWidth: 2,
          }
        },
        responsive: true,
      },
    });
  }
}

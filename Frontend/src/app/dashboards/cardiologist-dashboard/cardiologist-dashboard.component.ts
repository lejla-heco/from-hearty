import { Component, OnInit } from '@angular/core';
import { SharedModule } from '../../.shared/shared.module';
import { HttpClient } from '@angular/common/http';
import { Chart, registerables } from 'chart.js';
import { AppointmentService } from '../../appointment/appointment.service';
import { AuthentificationHelper } from '../../authentification/authentification-helper';
import { Config } from '../../configuration/config';
import { AiPredictionService } from '../../home-doctor/ai-prediction/ai-prediction.service';
import { monthLabels } from '../dashboards-config';
Chart.register(...registerables);

@Component({
  selector: 'app-cardiologist-dashboard',
  templateUrl: './cardiologist-dashboard.component.html',
  styleUrl: './cardiologist-dashboard.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class CardiologistDashboardComponent implements OnInit {
  public appointmentsPerMonthBarChart!: Chart;
  public appointmentsPerMonthDataset: any[] = [];

  public appointmentsPerMonthLineChart!: Chart;
  public approvedAppointmentsDataset: any[] = [];
  public pendingAppointmentsDataset: any[] = [];

  public urgentPatientsBarChart!: any;
  public urgentPatientsData: any = {};

  public userId = AuthentificationHelper.getLoginToken().userId;

  constructor(private httpClient: HttpClient, private aiPredictionService: AiPredictionService, private appointmentService: AppointmentService) {

  }

  ngOnInit() {
    this.getAppointmentsPerMonth();
    this.getApprovedAppointmentsPerMonth();
    this.getUrgentPatients();
  }

  getAppointmentsPerMonth(): void {
    this.httpClient.get(Config.serverAddress + this.appointmentService.api.approvedAppointmentsByCardiologistId + '/' + this.userId).subscribe((response: any) => {
      this.appointmentsPerMonthDataset = response.map((item: any) => ({
        ...item,
        borderWidth: 2
      }));
      this.initAppointmentsPerMonthBarChart();
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

  getUrgentPatients(): void {
    this.httpClient.get(Config.serverAddress + this.aiPredictionService.api.urgentPatients).subscribe((response: any) => {
      this.urgentPatientsData = response;
      this.initUrgentPatientsBarChart();
    });
  }

  initAppointmentsPerMonthBarChart(): void {
    this.appointmentsPerMonthBarChart = new Chart("approved-appointments-per-month", {
      type: 'bar',
      data: {
        labels: [''],
        datasets: this.appointmentsPerMonthDataset
      },
      options: {
        aspectRatio: 4,
        scales: {
          y: {
            beginAtZero: true,
            title: {
              display: true,
              text: 'Number of approved appointments'
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
            suggestedMax: 10
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

import { Component, OnInit } from '@angular/core';
import { PredictionRequest } from './models/prediction-request.model';
import { HttpClient } from '@angular/common/http';
import { Config } from '../../configuration/config';
import { AiPredictionService } from './ai-prediction.service';
import { ToastrService } from 'ngx-toastr';
import { SharedModule } from '../../.shared/shared.module';
import { SliderComponent } from '../../.shared/slider/slider.component';
import { AppointmentComponent } from '../../appointment/appointment.component';
import { PatientService } from '../../patient/patient.service';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { ActivatedRoute } from '@angular/router';
import { Patient } from '../../patient/models/patient.model';

@Component({
  selector: 'app-ai-prediction',
  templateUrl: './ai-prediction.component.html',
  styleUrls: ['./ai-prediction.component.css'],
  standalone: true,
  imports: [SharedModule, SliderComponent, AppointmentComponent],
  providers: [MdbModalService]
})
export class AiPredictionComponent implements OnInit {
  predictionMessage: string = '';
  appointment: boolean = false;
  loading: boolean = false;
  modalRef: MdbModalRef<AppointmentComponent> | null = null;
  patient?: Patient;

  constructor(
    private httpClient: HttpClient,
    public aiPredictionService: AiPredictionService,
    private toastr: ToastrService,
    public patientService: PatientService,
    private modalService: MdbModalService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.handleQueryParams();
    // Set the default value for the slider
    this.aiPredictionService.predictionRequest.chol = 3;
    this.aiPredictionService.predictionRequest.fbs = 3;
  }

  private handleQueryParams(): void {
    this.route.queryParams.subscribe(params => {
      if (params['patientId']) {
        this.getPatient(params['patientId']);
      }
    });
  }

  getPatient(id: any): void {
    this.httpClient.get(Config.serverAddress + this.patientService.api.patients + '/' + id).subscribe((response: any) => {
      this.patient = response;
    });
  }

  updateExangValue(event: any): void {
    this.aiPredictionService.predictionRequest.exang = event.target.checked ? 1 : 0;
  }

  clearPredictionForm(): void {
    this.aiPredictionService.predictionRequest = new PredictionRequest();
    this.toastr.info('Prediction form has been cleared!');
    this.predictionMessage = '';
    this.appointment = false;
  }

  predict(): void {
    if (this.validateFields()) {
      this.loading = true;
      this.httpClient
        .post(
          Config.serverAddress + this.aiPredictionService.api.predict,
          this.aiPredictionService.predictionRequest
        )
        .subscribe(
          (response: any) => {
            this.predictionMessage =
              'The likelihood percentage of the patient suffering from cardiovascular disease is: ' +
              response +
              '%';
            this.aiPredictionService.prediction = response;
            this.appointment = true;
          },
          (error) => {
            this.predictionMessage = 'An error occurred while predicting.';
          }
        )
        .add(() => (this.loading = false));
    }
  }

  predictWithOpenAI(): void {
    if (this.validateFields()) {
      this.loading = true;
      this.httpClient
        .post(
          Config.serverAddress + this.aiPredictionService.api.openAiPredict,
          this.aiPredictionService.predictionRequest
        )
        .subscribe(
          (response: any) => {
            this.predictionMessage = response.choices[0].text;
          },
          (error) => {
            this.predictionMessage = 'An error occurred while predicting with Open AI.';
          }
        )
        .add(() => (this.loading = false));
    }
  }

  validateFields(): any {
    if (
      /* this.aiPredictionService.predictionRequest.trestBps < 1 || */
      this.aiPredictionService.predictionRequest.chol < 1 ||
      this.aiPredictionService.predictionRequest.fbs < 1 ||
      this.aiPredictionService.predictionRequest.oldPeak < 1 ||
      this.aiPredictionService.predictionRequest.slope < 1 /* ||
      this.aiPredictionService.predictionRequest.ca < 1 */ /* ||
      this.aiPredictionService.predictionRequest.thal < 1 */
    ) {
      this.toastr.error(
        'Please ensure that numeric values are greater than or equal to 1!'
      );
      this.predictionMessage = '';
      return false;
    }
    return true;
  }

  getTextColor(): string {
    // Implement your logic to determine the color based on the slider value
    const cholValue = this.aiPredictionService.predictionRequest.chol;
    let cholColor = ""
    if (cholValue < 5) {
      cholColor = 'green'
    } else if (cholValue == 5) {
      cholColor = 'orange'
    } else if (cholValue > 5) {
      cholColor = 'red'
    }
    return cholColor
  }

  getTextColorSugar(): string {
    // Implement your logic to determine the color based on the slider value
    const sugarValue = this.aiPredictionService.predictionRequest.fbs;
    let sugarColor = ""
    if (sugarValue >= 3 && sugarValue <= 6) {
      sugarColor = 'green'
    } else if (sugarValue < 3) {
      sugarColor = 'red'
    } else if (sugarValue > 6 && sugarValue <= 9) {
      sugarColor = 'orange'
    } else if (sugarValue > 9) {
      sugarColor = 'red'
    }
    return sugarColor
  }

  getTextColorCa(): string {
    // Implement your logic to determine the color based on the slider value
    const caValue = this.aiPredictionService.predictionRequest.ca;
    let caColor = ""
    if (caValue == 0) {
      caColor = 'green'
    } else if (caValue == 1) {
      caColor = 'orange'
    } else if (caValue > 1) {
      caColor = 'red'
    }
    return caColor
  }

  getSelectOptionClass(optionValue: number): string {
    let optionClass = ''
    if (optionValue == 0) {
      optionClass = 'normal-option'
    } else if (optionValue == 1) {
      optionClass = 'bad-option'
    } else if (optionValue == 2) {
      optionClass = 'worst-option'
    }
    return optionClass
  }

  getSelectOptionThal(optionValue: number): string {
    let optionClass = ''
    if (optionValue == 0) {
      optionClass = 'normal-option'
    } else if (optionValue == 1) {
      optionClass = 'worst-option'
    }
    return optionClass
  }

  getSelectOptionClassHeartRate(optionValue: number): string {
    let optionClass = ''
    if (optionValue == 3) {
      optionClass = 'normal-option'
    } else if (optionValue == 6 || optionValue == 7) {
      optionClass = 'worst-option'
    }
    return optionClass
  }


  getSelectOptionClassPressure(optionValue: number): string {
    let optionClass = ''
    if (optionValue == 1 || optionValue == 2) {
      optionClass = 'normal-option'
    } else if (optionValue == 3 || optionValue == 4) {
      optionClass = 'bad-option'
    } else {
      optionClass = 'worst-option'
    }
    return optionClass
  }

  openModal(): void {
    this.modalRef = this.modalService.open(AppointmentComponent, {
      modalClass: 'modal-xl modal-dialog-scrollable'
    });
  }

}

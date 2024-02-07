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
import { ActivatedRoute, Router } from '@angular/router';
import { Patient } from '../../patient/models/patient.model';
import { AuthentificationHelper } from '../../authentification/authentification-helper';
import { RoleType } from '../../login/models/login-token.model';
import { ResultsPopupComponent } from './popups/results-popup/results-popup.component';
import { PredictionResult } from '../../calendar/models/prediction-result.model';

@Component({
  selector: 'app-ai-prediction',
  templateUrl: './ai-prediction.component.html',
  styleUrls: ['./ai-prediction.component.css'],
  standalone: true,
  imports: [SharedModule, SliderComponent, AppointmentComponent],
  providers: [MdbModalService]
})
export class AiPredictionComponent implements OnInit {

  formStep: number = 1;

  predictionMessage: string = '';
  appointment: boolean = false;
  loading: boolean = false;
  trestBpsLow: number = 0;
  patient?: Patient;
  modalRefResultsPopup: MdbModalRef<ResultsPopupComponent> | null = null;

  constructor(
    private httpClient: HttpClient,
    public aiPredictionService: AiPredictionService,
    private toastr: ToastrService,
    public patientService: PatientService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: MdbModalService
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
    console.log(this.aiPredictionService.predictionRequest.exang)
  }

  updateBloodValue(event: any): void {
    const newValue = event.target.value; 

    this.aiPredictionService.predictionRequest.trestBps = newValue;
  }


  updateBloodValueLow(event: any): void {
    const newValue = event.target.value; 

    this.trestBpsLow = newValue;
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
            this.openPredictModal(this.predictionMessage);
            this.aiPredictionService.prediction = response;
            this.appointment = true;
            this.createPredictionResult(response);
          },
          (error) => {
            this.predictionMessage = 'An error occurred while predicting.';
          }
        )
        .add(() => (this.loading = false));
    }
  }

  createPredictionResult(percentage: any): void {
    let predictionResult: PredictionResult = new PredictionResult(this.aiPredictionService.predictionRequest);
    predictionResult.percentage = percentage;
    predictionResult.patientId = this.patient!.id;
    predictionResult.houseDoctorId = AuthentificationHelper.getLoginToken().userId;
    predictionResult.label = Math.round(this.aiPredictionService.prediction!/100);

    this.httpClient.put(Config.serverAddress + this.aiPredictionService.api.predictionResult, predictionResult).subscribe((response: any) => {
      this.toastr.success("The created prediction has been stored in the Archive!");
    });
  }

  validateFields(showMessage: boolean = true): any {
    if (
      this.aiPredictionService.predictionRequest.trestBps < 1 || 
      this.trestBpsLow < 1 || 
      this.aiPredictionService.predictionRequest.chol < 1 ||
      this.aiPredictionService.predictionRequest.fbs < 1 ||
      this.aiPredictionService.predictionRequest.oldPeak < 1 ||
      this.aiPredictionService.predictionRequest.slope < 1 /* ||
      this.aiPredictionService.predictionRequest.ca < 1 */ /* ||
      this.aiPredictionService.predictionRequest.thal < 1 */
    ) {
      if (showMessage) {
        this.toastr.error(
          'Please ensure that numeric values are greater than or equal to 1!'
        );
      }
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

  getThalacColor(): string {
    // Implement your logic to determine the color based on the slider value
    const thalachValue = this.aiPredictionService.predictionRequest.thalach;
    let thalachColor = ""
    if (thalachValue < 60 || thalachValue > 100 ) {
      thalachColor = 'red'
    } else {
      thalachColor = 'green'
    }
    return thalachColor
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

  getSelectOptionClass(optionValue: any): string {
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

  getSelectOptionThal(optionValue: any): string {
    let optionClass = ''
    if (optionValue == 0) {
      optionClass = 'normal-option'
    } else if (optionValue == 1) {
      optionClass = 'worst-option'
    }
    return optionClass
  }

  getSelectOptionClassHeartRate(optionValue: any): string {
    let optionClass = ''
    if (optionValue == 3) {
      optionClass = 'normal-option'
    } else if (optionValue == 6 || optionValue == 7) {
      optionClass = 'worst-option'
    }
    return optionClass
  }

  getSelectOptionClassPressure(optionValue: any): string {
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

  goBack(): void {
    if (AuthentificationHelper.getLoginToken().roleType == RoleType.Doctor) this.router.navigate(['./patients']);
    else this.router.navigate(['/calendar']);
  }

  openPredictModal(predictionMessage: any): any {
    this.modalRefResultsPopup = this.modalService.open(ResultsPopupComponent, {
      modalClass: 'modal-lg',
      data: { predictionMessage: predictionMessage, patient: this.patient }
    });
  }


  //Old function
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

}

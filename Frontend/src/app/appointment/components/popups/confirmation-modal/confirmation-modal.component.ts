import { Component } from '@angular/core';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { SharedModule } from '../../../../.shared/shared.module';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.css',
  standalone: true,
  imports: [SharedModule]
})
export class ConfirmationModalComponent {
  public confirmationInfo: any;

  constructor(public modalRefConfirmationModal: MdbModalRef<ConfirmationModalComponent>){    
  }

  onYesButtonClick(): void {
    this.confirmationInfo.realise = true;
    this.modalRefConfirmationModal.close(this.confirmationInfo);
  }
}

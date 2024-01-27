import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SharedModule } from '../shared.module';

@Component({
  selector: 'slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css'],
  standalone: true,
  imports: [SharedModule],
})
export class SliderComponent {
  @Input() minValue = 1;
  @Input() maxValue = 100;
  @Input() value = 50;
  @Output() valueChange = new EventEmitter<number>();

  setValue(event: Event)
  {
    this.value = parseInt((event.target as any).value);
    this.valueChange.next(this.value);
  }
}

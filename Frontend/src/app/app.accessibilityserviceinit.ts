import { Injectable, Renderer2, RendererFactory2 } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccessibilityServiceInit {
  contrast = 'normal';
  textColor = '#000000'; 
  fontSize = 'medium'; 
  isColorReplaced = false;


  private renderer: Renderer2;

  constructor(private rendererFactory: RendererFactory2) {
    this.renderer = rendererFactory.createRenderer(null, null);
  }

  applySettings() {
    const body = document.body;
      this.applyStyles(body);
  }

  private applyStyles(element: HTMLElement) {
    this.renderer.setStyle(element, 'background-color', this.getContrastColor());

    this.renderer.setStyle(element, 'color', this.textColor);

    this.renderer.setStyle(element, 'font-size', this.getFontSize());

    const divTag = document.querySelectorAll('div');
    if (divTag) {
        divTag.forEach((divElement: HTMLElement) => {
            this.renderer.setStyle(divElement, 'color', 'black');
            this.renderer.setStyle(divElement, 'font-weight', 'bold');
        });
    }

    const customTab2Element = document.querySelector('.customtab2');
    if(customTab2Element) {
        const elementsWithAriaSelectedFalse = customTab2Element.querySelectorAll('[aria-selected="false"]');

        elementsWithAriaSelectedFalse.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#f5582a');
        });

    }

    const calendarTag = document.querySelectorAll('.fc-col-header-cell a');
    if (calendarTag) {
        calendarTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#f5582a');
        });
    }

    const breadCrumbTag = document.querySelectorAll('.breadcrumb-item.active');
    if (breadCrumbTag) {
        breadCrumbTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#01767a');
        });
    }

    const fcTag = document.querySelectorAll('.fc-event');
    if (fcTag) {
        fcTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'background', '#01767a');
        });
    }

    const fcTagMain = document.querySelectorAll('.fc-event-main');
    if (fcTagMain) {
        fcTagMain.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#fff');
        });
    }

    const toastTag = document.querySelectorAll('#toast-container');
    if (toastTag) {
        toastTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#fff');
        });
    }

    const h4Tag = document.querySelectorAll('h4');
    if (h4Tag) {
        h4Tag.forEach((divElement: HTMLElement) => {
            this.renderer.setStyle(divElement, 'color', 'black');
            this.renderer.setStyle(divElement, 'font-weight', 'bold');
        });
    }

    const headerTag = document.querySelectorAll('header');
    if (headerTag) {
        headerTag.forEach((headerElement: HTMLElement) => {
            this.renderer.setStyle(headerElement, 'background', '#01767a');
        });
    }

    const sidebarNav = document.getElementById('sidebarnav');
    if (sidebarNav) {
      const anchorTag = sidebarNav.querySelectorAll('a');
      const spanTag = sidebarNav.querySelectorAll('span');
      const liTag = sidebarNav.querySelectorAll('li');
      const iconTag = sidebarNav.querySelectorAll('i');


      if (anchorTag) {
        anchorTag.forEach((spanElement: HTMLElement) => {
            this.renderer.setStyle(spanElement, 'color', 'black');
            this.renderer.setStyle(spanElement, 'font-weight', 'bold');
        });
      }

      if (liTag) {
        liTag.forEach((spanElement: HTMLElement) => {
            this.renderer.setStyle(spanElement, 'color', 'black');
            this.renderer.setStyle(spanElement, 'font-weight', 'bold');
        });
      }


      if(iconTag) {
        iconTag.forEach((spanElement: HTMLElement) => {
            this.renderer.setStyle(spanElement, 'color', 'black');
        });
      }

      if(spanTag){
        spanTag.forEach((spanElement: HTMLElement) => {
            this.renderer.setStyle(spanElement, 'color', 'black');
            this.renderer.setStyle(spanElement, 'font-weight', 'bold');
        });
      }
    }
  }

  private getContrastColor() {
    return this.contrast === 'normal' ? '#ffffff' : '#000000';
  }

  private getFontSize() {
    return this.fontSize === 'small' ? '12px' : this.fontSize === 'medium' ? '16px' : '20px';
  }
}
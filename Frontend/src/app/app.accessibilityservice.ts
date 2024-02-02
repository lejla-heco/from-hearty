import { Injectable, Renderer2, RendererFactory2 } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccessibilityService {
  // Default settings
  contrast = 'normal';
  textColor = '#000000'; 
  fontSize = 'medium';
  isColorReplaced = false;


  private renderer: Renderer2;
  private stylesApplied = false;

  constructor(private rendererFactory: RendererFactory2) {
    this.renderer = rendererFactory.createRenderer(null, null);
  }

  applySettings() {
    const body = document.body;

    if (this.stylesApplied) {
      this.removeStyles(body);
    } else {
      this.applyStyles(body);
    }

    this.stylesApplied = !this.stylesApplied;
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

    const labelTag = document.querySelectorAll('label');
    if (labelTag) {
        labelTag.forEach((divElement: HTMLElement) => {
            this.renderer.setStyle(divElement, 'color', 'black');
            this.renderer.setStyle(divElement, 'font-weight', 'bold');
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

    const fcTagMain = document.querySelectorAll('.fc-event-main');
    if (fcTagMain) {
        fcTagMain.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#fff');
        });
    }

    const customTab2Element = document.querySelector('.customtab2');
    if(customTab2Element) {
        const elementsWithAriaSelectedFalse = customTab2Element.querySelectorAll('[aria-selected="false"]');

        elementsWithAriaSelectedFalse.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#f5582a');
        });

        const activeTab = customTab2Element.querySelectorAll('.nav-link.active');

        activeTab.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.setStyle(el, 'color', '#4f5467');
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

  private removeStyles(element: HTMLElement) {
    this.renderer.removeStyle(element, 'background-color');

    this.renderer.removeStyle(element, 'color');

    this.renderer.removeStyle(element, 'font-size');

    const fcTag = document.querySelectorAll('.fc-event');
    if (fcTag) {
        fcTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'background');
        });
    }

    const fcTagMain = document.querySelectorAll('.fc-event-main');
    if (fcTagMain) {
        fcTagMain.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'color');
        });
    }

    const toastTag = document.querySelectorAll('#toast-container');
    if (toastTag) {
        toastTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'color');
        });
    }

    const breadCrumbTag = document.querySelectorAll('.breadcrumb-item.active');
    if (breadCrumbTag) {
        breadCrumbTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'color');
        });
    }

    const calendarTag = document.querySelectorAll('.fc-col-header-cell a');
    if (calendarTag) {
        calendarTag.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'color');
        });
    }


    const customTab2Element = document.querySelector('.customtab2');
    if(customTab2Element) {
        const elementsWithAriaSelectedFalse = customTab2Element.querySelectorAll('[aria-selected="false"]');

        elementsWithAriaSelectedFalse.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'color');
        });

        const activeTab = customTab2Element.querySelectorAll('.nav-link.active');

        activeTab.forEach((headerElement: Element) => {
            const el = headerElement as HTMLElement;
            this.renderer.removeStyle(el, 'color');
        });
    }

    const headerTag = document.querySelectorAll('header');
    if (headerTag) {
        headerTag.forEach((headerElement: HTMLElement) => {
            this.renderer.removeStyle(headerElement, 'background');
        });
    }

    const divTag = document.querySelectorAll('div');
    if (divTag) {
        divTag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
            this.renderer.removeStyle(spanElement, 'font-weight');
        });
    }

    const labelTag = document.querySelectorAll('label');
    if (labelTag) {
        labelTag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
            this.renderer.removeStyle(spanElement, 'font-weight');
        });
    }

    const h4Tag = document.querySelectorAll('h4');
    if (h4Tag) {
        h4Tag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
            this.renderer.removeStyle(spanElement, 'font-weight');
        });
    }

    const sidebarNav = document.getElementById('sidebarnav');
    if (sidebarNav) {
      const anchorTag = sidebarNav.querySelectorAll('a');
      const iconTag = sidebarNav.querySelectorAll('i');
      const spanTag = sidebarNav.querySelectorAll('span');
      const liTag = sidebarNav.querySelectorAll('li');


      if (anchorTag) {
        anchorTag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
            this.renderer.removeStyle(spanElement, 'font-weight');
        });
      }

      if (liTag) {
        liTag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
            this.renderer.removeStyle(spanElement, 'font-weight');
        });
      }


      if(iconTag) {
        iconTag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
        });
      }

      if(spanTag){
        spanTag.forEach((spanElement: HTMLElement) => {
            this.renderer.removeStyle(spanElement, 'color');
            this.renderer.removeStyle(spanElement, 'font-weight');
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
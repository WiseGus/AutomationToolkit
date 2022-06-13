import { Directive, ElementRef, Renderer2, Input, SimpleChanges, OnChanges } from '@angular/core';

@Directive({
  selector: '[appDisableElement]'
})
export class DisableElementDirective implements OnChanges {
  @Input() appDisableElement: boolean;

  constructor(private _elem: ElementRef, private _renderer: Renderer2) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.appDisableElement) {
      this._renderer.setAttribute(this._elem.nativeElement, 'disabled', 'true');
    } else {
      this._renderer.setAttribute(this._elem.nativeElement, 'disabled', null);
    }

  }

}

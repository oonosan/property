import { Component, ElementRef, HostListener, OnInit, Renderer2, ViewChild } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  @ViewChild('moon') moon!: ElementRef;
  @ViewChild('stars') stars!: ElementRef;
  @ViewChild('buildings') buildings!: ElementRef;
  
  constructor(private renderer: Renderer2) { 
  }

  ngOnInit(): void {
  }
  
  @HostListener("window:scroll", ["$event"])
  scrollLandscape($event:Event){
    let value = window.scrollY;
    this.renderer.setStyle(this.moon.nativeElement, 'transform', 'translateY(' + value * 2 + 'px)');
    this.renderer.setStyle(this.stars.nativeElement, 'transform', 'translateY(' + value * 1.05 + 'px)');
    this.renderer.setStyle(this.buildings.nativeElement, 'transform', 'translateY(' + value * 1.05 + 'px)');
    /*
    mountains_back.style.top = value * 0.5 + 'px';
    mountains_front.style.top = value * 0 + 'px';
    text.style.marginRight = value * 4 + 'px';
    text.style.marginTop = value * 1.5 + 'px';
    btn.style.marginTop = value * 1.5 + 'px';
    */
    console.log("scroll: ", value * 2.05 + 'px');
  }
}

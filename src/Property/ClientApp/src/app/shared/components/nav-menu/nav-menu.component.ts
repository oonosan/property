import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  @ViewChild('burgerMenu') burgerMenu!: ElementRef;
  @ViewChild('navMenu') navMenu!: ElementRef;

  constructor() { }

  ngOnInit(): void {
  }

  mobileMenu(): void {
    this.burgerMenu.nativeElement.classList.toggle('active');
    this.navMenu.nativeElement.classList.toggle('active');
  }
}

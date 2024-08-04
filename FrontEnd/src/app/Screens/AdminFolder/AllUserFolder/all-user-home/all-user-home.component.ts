import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-all-user-home',
  templateUrl: './all-user-home.component.html',
  styleUrls: ['./all-user-home.component.css']
})
export class AllUserHomeComponent implements OnInit {
  public OpenChildren:number=1;
  constructor() { }

  ngOnInit(): void {
  }

}

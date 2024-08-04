import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-subcategory-dashboard',
  templateUrl: './subcategory-dashboard.component.html',
  styleUrls: ['./subcategory-dashboard.component.css']
})
export class SubcategoryDashboardComponent implements OnInit {
  public OpenChildren:number=1;
  constructor() { }

  ngOnInit(): void {
  }

}

import { isNull } from '@angular/compiler/src/output/output_ast';
import { ResponseValidation } from '../../../../../Model/ResponseValidation';
import { CategoryService } from '../../../../../Services/Category.service';
import { LoginService } from '../../../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { CategoryValidationService } from 'src/app/Services/CategoryValidation.service';

@Component({
  selector: 'app-category-dashboard',
  templateUrl: './category-dashboard.component.html',
  styleUrls: ['./category-dashboard.component.css']
})
export class CategoryDashboardComponent implements OnInit {
  public OpenChildren:number=1;
  constructor() { }

  ngOnInit(): void {
  }
}

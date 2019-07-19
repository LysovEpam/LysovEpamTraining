import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from 'src/Services/api-settings.service';
import { Router } from '@angular/router';
import { ProductDataRequest } from 'src/model/entities/apiRequests/productDataRequest';
import { ProductSearchRequest } from 'src/model/entities/apiRequests/productSearchRequest';
import { ProductStatusEnum } from 'src/model/entities/apiEntities/additional/productStatus';

@Component({
  selector: 'app-editor-dashboard',
  templateUrl: './editor-dashboard.component.html',
  styleUrls: ['./editor-dashboard.component.css']
})
export class EditorDashboardComponent implements OnInit {

  constructor(private router: Router, private localStorageService:LocalStorageService, private http: HttpClient, 
    private apiSettings:ApiSettingsService) { }

  ngOnInit() {
  }

  productCategoryCreate(){
    this.router.navigate(['/product-category'], {
      queryParams:{
        'action': 'create'} 
    });
  }
  
  productCategoryList(){
    this.router.navigate(['/product-categories']);
  }

  productInformationCreate(){
    this.router.navigate(['/product-information'], {
      queryParams:{
        'action': 'create'} 
    });
  }
  
  productInformationList(){
    this.router.navigate(['/product-informations']);
  }

  productCreate(){
    this.router.navigate(['/product'], {
      queryParams:{
        'action': 'create'} 
    });
  }
 
  productList(){
    this.router.navigate(['/products'], {
      queryParams:{
        'action': 'changeAll'}
    });
  }

 



}

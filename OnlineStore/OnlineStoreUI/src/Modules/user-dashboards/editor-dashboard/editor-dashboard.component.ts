import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/services/localstorage.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from 'src/services/api-settings.service';
import { Router } from '@angular/router';
import { UserRoleEnum } from 'src/model/entities/apiEntities/additional/userRole';

@Component({
  selector: 'app-editor-dashboard',
  templateUrl: './editor-dashboard.component.html',
  styleUrls: ['./editor-dashboard.component.css']
})
export class EditorDashboardComponent implements OnInit {

  constructor(private router: Router, private localStorageService:LocalStorageService, private http: HttpClient, 
    private apiSettings:ApiSettingsService) { }

  ngOnInit() {
    let userRoleName: string = this.localStorageService.getUserRole();
    let role: UserRoleEnum = UserRoleEnum[userRoleName];
    let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];

    if(userRoleEnum != UserRoleEnum.Admin && 
      userRoleEnum != UserRoleEnum.Editor)
    {
      this.router.navigate(['/singin'], {
        queryParams:{ 'authorizationMessage': 
        'You do not have permissions, authorization is required to access the editor panel'}});
    }
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

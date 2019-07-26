import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/services/localstorage.service';
import { Router } from '@angular/router';
import { SystemUserData } from 'src/model/entities/apiEntities/systemUserData';
import { UserServerService } from 'src/services/user-server.service';
import { ServerErrorsService } from 'src/services/server-errors.service';
import { UserRoleEnum } from 'src/model/entities/apiEntities/additional/userRole';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {

  accountMessage:string;

  systemUserData:SystemUserData;


  constructor(private router: Router,
    private localStorageService: LocalStorageService, 
    private userServerService:UserServerService, 
    private serverErrorService: ServerErrorsService) { }

  ngOnInit() {
    
  }

  showUserOrders(){
    this.router.navigate(['/orders'], {
      queryParams:{
        'action': 'showMyOrders'}
    });
  }

  updateUserInformation(){
    this.router.navigate(['/user']);
  }










}

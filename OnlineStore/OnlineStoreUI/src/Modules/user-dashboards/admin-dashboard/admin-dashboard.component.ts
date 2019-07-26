import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/services/localstorage.service';
import { UserRoleEnum } from 'src/model/entities/apiEntities/additional/userRole';
import { Router } from '@angular/router';
import { OrderStatusEnum } from 'src/model/entities/apiEntities/additional/orderStatus';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {

  constructor(private router: Router,private localStorageService: LocalStorageService) { }

  ngOnInit() {
    let userRoleName: string = this.localStorageService.getUserRole();
    let role: UserRoleEnum = UserRoleEnum[userRoleName];
    let userRoleEnum: UserRoleEnum = (<any>UserRoleEnum)[role];

    if(userRoleEnum != UserRoleEnum.Admin)
    {
      this.router.navigate(['/singin'], {
        queryParams:{ 'authorizationMessage': 'You do not have permissions, authorization is required to access the admin panel'}});
    }
  }

  
  ordersShowFilter(){

      var statusName:string = OrderStatusEnum[OrderStatusEnum.NewOrder].toString();

      this.router.navigate(['orders-filters/orders'], {
          queryParams:{
            'action': 'selectFilter',
            'search': '',
            'status': statusName
          }
        });
  
  }

  orderShowAll(){
    this.router.navigate(['/orders'], {
      queryParams:{
        'action': 'changeAll'
      }
    });
  }

  usersShowAll(){
    this.router.navigate(['/users']);
  }

  userCreateNew(){
    this.router.navigate(['/user'], {
      queryParams:{
        'action': 'create'
      }
    });
  }
  


}

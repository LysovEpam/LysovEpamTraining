import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/Services/localstorage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {

  constructor(private router: Router,private localStorageService: LocalStorageService) { }

  ngOnInit() {
    if(this.localStorageService.getAuthorizationWordDate() < new Date(Date.now()))
    {
      this.router.navigate(['/singin'], {
        queryParams:{
          'authorizationMessage': "Access required authorization"}
      });
    }
  }

}

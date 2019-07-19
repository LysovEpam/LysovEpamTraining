import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { SystemUserData } from 'src/model/entities/apiEntities/systemUserData';


@Injectable({
  providedIn: 'root'
})
export class UserServerService {

    constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    

    getByMyself(jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlUserGetByMyself(), '', httpOptions);
    }

    updateByMyself(data: SystemUserData, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlUserUpdateByMyself(), data, httpOptions);

    }

    
}

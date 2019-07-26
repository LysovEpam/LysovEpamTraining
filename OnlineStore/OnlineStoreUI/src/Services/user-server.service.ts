import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { SystemUserData } from 'src/model/entities/apiEntities/systemUserData';


@Injectable({
  providedIn: 'root'
})
export class UserServerService {

    constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    getAll(jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlUserGetAll(), '', httpOptions);
    }

    getById(id:number, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlUserGetById(), id, httpOptions);
    }

    getByToken(jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlUserGetByToken(), '', httpOptions);
    }

    create(userData: SystemUserData, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlUserCreate(), userData, httpOptions);

    }

    update(userData: SystemUserData, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.post(this.apiSettings.getUrlUserUpdate(), userData, httpOptions);

    }

    delete(id: number, jwt:string){

      var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

      const httpOptions = {
        headers: headers_object
      };

      return this.http.delete(this.apiSettings.getUrlUserDelete()+'/' + id, httpOptions); 
    }


    
}

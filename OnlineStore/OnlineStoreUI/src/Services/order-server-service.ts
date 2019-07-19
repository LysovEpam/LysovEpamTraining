import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ApiSettingsService } from './api-settings.service';
import { OrderRequest } from 'src/model/entities/apiRequests/orderRequest';


@Injectable({
  providedIn: 'root'
})
export class OrderServerService {

    constructor(private http: HttpClient, private apiSettings:ApiSettingsService){ }
      
    

    getById(id:number, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlOrderGetById(), id, httpOptions);
    }

    create(order: OrderRequest, jwt:string){

        var headers_object = new HttpHeaders().set("Authorization", "Bearer " + jwt);

        const httpOptions = {
          headers: headers_object
        };

        return this.http.post(this.apiSettings.getUrlOrderCreate(), order, httpOptions);

    }

    
}

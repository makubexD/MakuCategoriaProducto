import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from './../../../environments/environment';
@Injectable()
export class CategoryService {

    constructor(private http: HttpClient) {
    }
    

    public getAll(filterValue: string): Observable<any> {
        const url = environment.serviceUrl + '/api/category/search?filterValue=' + filterValue;
        return this.http.get<any>(url);
    }
}
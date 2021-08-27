import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PhoneNumberType } from '../_models/PhoneNumberType';

@Injectable({
  providedIn: 'root'
})

export class TypeService {

  baseURL = 'https://localhost:5001/v1/phonetypes';

  constructor(private http: HttpClient) { }

  getAllTypes(): Observable<PhoneNumberType[]> {
    return this.http.get<PhoneNumberType[]>(this.baseURL);
  }

  getTypesById(id: number): Observable<PhoneNumberType> {
    return this.http.get<PhoneNumberType>(`${this.baseURL}/${id}`);
  }

  postType(type: PhoneNumberType) {
    return this.http.post(this.baseURL, type);
  }

  putType(type: PhoneNumberType) {
    return this.http.put(`${this.baseURL}/${type.id}`, type);
  }

  deleteType(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}

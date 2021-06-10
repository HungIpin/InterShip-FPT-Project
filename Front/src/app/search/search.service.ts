import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Certification } from '../home/model';
import {map} from 'rxjs/operators';
import {BehaviorSubject, Observable} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  private currentCertificationSubject: BehaviorSubject<Certification>;
  public currentCertification: Observable<Certification>;

  private urlAPI = 'https://localhost:44374';

  constructor(private  http:HttpClient)
  {
    this.currentCertificationSubject = new BehaviorSubject<Certification>(
        JSON.parse(localStorage.getItem('currentCertification'))
    );
    this.currentCertification = this.currentCertificationSubject.asObservable();
  }
  public get currentProductValue(): Certification{
      return this.currentCertificationSubject.value;
  }  

  public getsearchcertificate = (name: string) => {
    const getUrl = `${this.urlAPI}/api/Certifications/GetCertification/${name}`;
        return this.http.get<any>(getUrl).pipe(
            map((certificates) => {
                if(certificates != null)
                {
                    const getcertificate = [];
                    certificates.forEach(element => {
                        getcertificate.push(element);
                        this.currentCertificationSubject.next(element);
                    });
                    return getcertificate;
                }
                else{
                    return null;
                }
            })
        )
  }

  public getAllCertificate = () =>{
    const getProductUrl = `${this.urlAPI}/api/certifications/`;
    return this.http.get<any>(getProductUrl).pipe(
        map((certificates) => {
            if(certificates != null)
            {
                const getCertificate = [];
                certificates.forEach(element => {
                  getCertificate.push(element);
                    this.currentCertificationSubject.next(element);
                });
                return getCertificate;
            }
            else{
                return null;
            }
        })
    )
}
}

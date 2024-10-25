import {Component, inject, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public heroes: Hero[] = [];

  http = inject(HttpClient)
  baseUrl: string = ''
  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl
    this.fetchHeroes()
  }

fetchHeroes(){
  this.http.get<Hero[]>(this.baseUrl + 'heroes').subscribe(result => {
    console.log(result)
    this.heroes = result;
  }, error => console.error(error));
}

  disableHero(id:Number){

    this.http.delete(this.baseUrl + 'heroes/' + id).subscribe(result => {
      // console.log(id)
      // console.log(result)
      this.fetchHeroes()
      }, error =>
        {
          // console.error(error)
        }
      );
  }

}

interface Hero {
  Id : Number;
  Name: String;
  Alias: String;
  Brand: String;
}

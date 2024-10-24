import {Component, Inject, inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-add-hero',
  templateUrl: './add-hero.component.html',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  styleUrls: ['./add-hero.component.css']
})

export class AddHeroComponent {

  http = inject(HttpClient)
  baseUrl: string = ''
  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl
  }

  form = new FormGroup({
    name : new FormControl<string | null>(null, Validators.required),
    alias: new FormControl<string | null>(null, Validators.required),
    brand: new FormControl<string | null>(null, Validators.required),
  })

  onSubmit(){
    const fd = new FormData()
    if(this.form.valid){

      fd.append('name', this.form.value.name!)
      fd.append('alias', this.form.value.alias!)
      fd.append('brand', this.form.value.brand!)

      this.http.post(this.baseUrl + 'heroes', fd ).subscribe(result => {
      },error => console.error(error));
    }
  }
}

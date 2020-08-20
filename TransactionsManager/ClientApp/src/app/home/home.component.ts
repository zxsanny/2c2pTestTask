import { Component, Inject } from '@angular/core';
import { NotifierService } from "angular-notifier";
import UploadResult from '../../models/UploadResult';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(private notifierService: NotifierService, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  reloadTransactions = () => {
    this.http.get(this.baseUrl + 'transaction').subscribe(data => {
      //view data
    });
  };

  onUpload = (event: UploadResult) => {
    if (!event.ParserResult.success) {
      this.notifierService.show({
        message: event.ParserResult.errors.join(', '),
        type: "error"
      });
    }
    else {
      this.reloadTransactions();
    }
  };
}

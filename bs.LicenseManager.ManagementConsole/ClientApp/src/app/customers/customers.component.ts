import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../_services/customer.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {

  vm:any;

  constructor(private customerService: CustomerService) { }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.customerService.getCustomers()
      .subscribe({
        next: (data) => {this.vm = data; console.log(data);},
        error: (error) => {console.log('Errore caricando i dati:',error)},
        complete: () => {}
      });
  }
}

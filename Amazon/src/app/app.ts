import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SearchHeader } from './layout/search-header/search-header';
import { Navbar } from './layout/navbar/navbar';
import { ProductCard } from './layout/product-card/product-card';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,SearchHeader,Navbar,ProductCard],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'AngularStructure';
}

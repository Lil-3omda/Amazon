import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchHeader } from './search-header';

describe('SearchHeader', () => {
  let component: SearchHeader;
  let fixture: ComponentFixture<SearchHeader>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SearchHeader]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SearchHeader);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

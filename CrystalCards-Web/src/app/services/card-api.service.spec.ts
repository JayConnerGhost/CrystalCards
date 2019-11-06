import { HttpClientTestingModule,   HttpTestingController } from '@angular/common/http/testing';
import { TestBed, getTestBed } from '@angular/core/testing';
import { ConfigService } from './config.service';
import { CardApiService } from './card-api.service';

describe('CardApiService', () => {
  let service: CardApiService;
  let injector: TestBed;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CardApiService, ConfigService]
    });
    // inject the service
    injector = getTestBed();
    service = injector.get(CardApiService);
    httpMock = injector.get(HttpTestingController);
  });


  it('should get cards successfully',()=> {
    service.getCards().subscribe();
    const req = httpMock.expectOne('http://localhost:50872/api/cards/GetForUserName/null');
  });

  afterEach(() => {
    httpMock.verify();
  });
});

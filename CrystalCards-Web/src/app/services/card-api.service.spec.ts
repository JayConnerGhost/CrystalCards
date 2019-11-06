import { HttpClientTestingModule,   HttpTestingController } from '@angular/common/http/testing';
import { TestBed, getTestBed } from '@angular/core/testing';
import { ConfigService } from './config.service';
import { CardApiService } from './card-api.service';
import {Card} from "../card";
import {AuthService} from "./auth.service";

class AuthServiceStub extends AuthService{
  public  getUserName():string
  {
    return "jade"
  }
  public loggedIn():boolean{
    return true;
  }
}


describe('CardApiService', () => {
  let service: CardApiService;
  let injector: TestBed;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CardApiService,
        ConfigService,
        {provide: AuthService, useClass: AuthServiceStub}
        ]
    }).compileComponents();
    // inject the service
    injector = getTestBed();
    service = injector.get(CardApiService);
    httpMock = injector.get(HttpTestingController);
  });

  it('should get cards successfully',()=> {
    //set up return data
    let cardData : Card[]= [
      {"id":0, "title": "test card ", "description": "test card description",actionPoints:null,links:null,npPoints:null,order:0}
    ];
    service.getCards().subscribe(response=>{
      expect(response).toBe(cardData);
    });

    const req = httpMock.expectOne('http://localhost:50872/api/cards/GetForUserName/jade');
    expect(req.request.method).toBe('GET');
    req.flush(cardData);
  });

  it('URL should include username',()=>{
    let userName='jade';
    service.getCards().subscribe();
    const req = httpMock.expectOne(`http://localhost:50872/api/cards/GetForUserName/${userName}`);
    expect(req.request.method).toBe('GET');
  });

  afterEach(() => {
    httpMock.verify();
  });
});

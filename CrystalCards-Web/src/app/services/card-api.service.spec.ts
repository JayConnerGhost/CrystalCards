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

  it('GetCards, should get cards successfully',()=> {
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
  it('GetCards, URL should include username',()=>{
    let userName='jade';
    service.getCards().subscribe();
    const req = httpMock.expectOne(`http://localhost:50872/api/cards/GetForUserName/${userName}`);
    expect(req.request.method).toBe('GET');
  });
  it('AddBasicCard, should return a card with correct values',()=>{
    const title="test card title";
    const description ='test card description';
    const id=0;
    service.newBasic(id,title,description).subscribe(response=>{
      expect(response.title).toBe(title);
      expect(response.description).toBe(description);

    });
    const req = httpMock.expectOne(`http://localhost:50872/api/cards/jade`);
    expect(req.request.method).toBe('POST');
    let card=new Card();
    card.title=title;
    card.description=description;
    req.flush(card);

  });
  it('DeleteCard, correct url is called with correct method',()=>{
    let cardId=1;
    service.deleteCard(cardId).subscribe();
    const req = httpMock.expectOne(`http://localhost:50872/api/cards/${cardId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(cardId);
  });
  it('UpdateCard, should return a card with correct values',()=>{
    let title='test card';
    let description='test card description';
    let id=1;
    let actionPoints=[];
    let npPoints=[];

    service.update(title,description,id,npPoints,actionPoints,[]).subscribe(response=>{
      expect(response.title).toBe(title);
      expect(response.description).toBe(description);
      expect(response.id).toBe(id);
      });
    const req = httpMock.expectOne(`http://localhost:50872/api/cards/${id}`);
    expect(req.request.method).toBe('PUT');
    let card=new Card();
    card.description=description;
    card.title=title;
    card.id=id;
    req.flush(card);
  });

  afterEach(() => {
    httpMock.verify();
  });
});

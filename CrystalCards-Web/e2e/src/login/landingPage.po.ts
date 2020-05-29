describe('Landing page',()=>{

  browser.driver.get('http://localhost:4444/wd/hub')

  it('Should find correct title', function(){
    expect(browser.driver.getTitle()).toEqual('Crystal Ideas');
  });

});

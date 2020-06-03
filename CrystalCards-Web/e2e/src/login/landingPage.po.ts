

describe('Crystal Ideas Login Process', function() {
  it('should have be able to log me in',  function() {
    const baseUrl = 'http://localhost:4200/';
    browser.driver.manage().window().maximize() ;
  browser.get(baseUrl);
    browser.executeScript('window.sessionStorage.clear();');
    browser.executeScript('window.localStorage.clear();');
  browser.waitForAngular();
  browser.sleep(1000);
  element(by.css('#loginButton')).click();

   browser.waitForAngular();

   element(by.id('usernameField')).sendKeys('Test');
   element(by.id('passwordField')).sendKeys('test');
   element(by.id('loginButton')).click();


    browser.waitForAngular();
    expect(element(by.id('welcomeContainer')).getText()).toEqual('Welcome Test');
    //element(by.id('logoutButton')).click();
    browser.executeScript('window.sessionStorage.clear();');
    browser.executeScript('window.localStorage.clear();');
    browser.waitForAngular();
  });
});

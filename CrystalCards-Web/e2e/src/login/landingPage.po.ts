

describe('Crystal Ideas Login Process', function() {
  it('should have be able to log me in', function() {
    const baseUrl = 'http://localhost:4200/';
     browser.get(baseUrl);
     element(by.id('loginButton')).click();
    browser.driver.sleep(1000);
    browser.waitForAngular();

    element(by.id('usernameField')).sendKeys('Test');
    element(by.id('passwordField')).sendKeys('test');
    element(by.id('loginButton')).click();

    browser.driver.sleep(1000);
    browser.waitForAngular();
    expect(element(by.id('welcomeContainer')).getText()).toEqual('Welcome Test');
  });
});

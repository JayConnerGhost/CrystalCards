exports.config = {
  framework: 'jasmine',
  seleniumAddress: 'http://127.0.0.1:4444/wd/hub',
  specs: [
     './login/landingPage.po.ts',
    './tools/sessionStorageStub.po.ts',
    './AddCard/AddIdea.po.ts',
    './tools/sessionStorageStub.po.ts',

  ]
}

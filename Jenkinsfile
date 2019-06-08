pipeline {
  agent any
  stages {
    stage('Restore PACKAGES') {
      steps {
        bat "dotnet restore"
       }
    }
    stage('Clean') {
      steps {
        bat 'dotnet clean'
      }
    }
  stage('Build') {
     steps {
      bat 'dotnet build'
      }
    }
  stage('Build + SonarQube analysis') {
    def sqScannerMsBuildHome = tool 'Scanner for MSBuild 2.2'
    withSonarQubeEnv('My SonarQube Server') {
      // Due to SONARMSBRU-307 value of sonar.host.url and credentials should be passed on command line
      bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe begin /k:myKey /n:myName /v:1.0 /d:sonar.host.url=%SONAR_HOST_URL% /d:sonar.login=%SONAR_AUTH_TOKEN%"
      bat 'MSBuild.exe /t:Rebuild'
      bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe end /d:sonar.login=%SONAR_AUTH_TOKEN%"
    }
  }
  }
}

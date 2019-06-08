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
   stage('Sonarqube') {
    environment {
        scannerHome = tool 'dotnetcorescanner'
    }
    steps {
        withSonarQubeEnv('sonarqube') {
            sh "${scannerHome}/bin/sonar-scanner"
        }
        timeout(time: 10, unit: 'MINUTES') {
            waitForQualityGate abortPipeline: true
        }
    }
}
  }
}

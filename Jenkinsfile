pipeline {
  agent any
  stages {
    stage('Restore PACKAGES') {
      steps {
        bat 'dotnet restore'
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
    stage('Sonar') {
      steps {
        withSonarQubeEnv(installationName: 'dotnetcorescanner', credentialsId: 'd79705dd-b88b-474b-8cd6-9ed11270dd63')
      }
    }
  }
}
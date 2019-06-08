pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git(url: 'https://github.com/kkularatne/ASP.NET-core-in-memory-cahche.git', branch: 'master', credentialsId: 'userid')
      }
    }
  }
}
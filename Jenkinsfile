pipeline {
    agent any

    stages {
        stage('Hello') {
            steps {
                echo 'Hello World'
            }
        }
        
        stage('DotNetClean') {
            steps {
                dotnetClean configuration: 'Debug', project: 'C:\\Users\\ajm72\\.jenkins\\workspace\\Test_Freestyle\\PlaywrigthDotNet\\PlaywrightUdemy'
            }
        }
        
        stage('DotNetBuild') {
            steps {
                dotnetBuild configuration: 'Debug', project: 'C:\\Users\\ajm72\\.jenkins\\workspace\\Test_Freestyle\\PlaywrigthDotNet\\PlaywrightUdemy', sdk: '.Net8.0'
            }
        }
        
        stage('RunTests') {
            steps {
                dotnetTest configuration: 'Debug', project: 'C:\\Users\\ajm72\\.jenkins\\workspace\\Test_Freestyle\\PlaywrigthDotNet\\PlaywrightUdemy', sdk: '.Net8.0'
            }
        }
    }
}

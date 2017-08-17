import QtQuick 2.7
import QtQuick.Controls 2.0
import QtQuick.Layouts 1.0
import test 1.1
import test.io 1.0

ApplicationWindow {
    visible: true
    width: 640
    height: 480
    title: qsTr("Hello World")

    SwipeView {
        id: swipeView
        anchors.fill: parent
        currentIndex: tabBar.currentIndex

        Page1 {
        }

        Page {
            Label {
                text: qsTr("Second page")
                anchors.centerIn: parent
            }
        }
    }

	TestQmlImport {
		Component.onCompleted: {
			{
				console.log("Test")
				console.log(testt.TestPropertyBool)
				console.log(testt.TestPropertyBool2)
				console.log(testt.TestMethodReturnInt())
				console.log(testt.TestMethodReturnIntParamInt(234234))
			}
		}
		id: testt
	}

    footer: TabBar {
        id: tabBar
        currentIndex: swipeView.currentIndex
        TabButton {
            text: qsTr("First")
        }
        TabButton {
            text: qsTr("Second")
        }
    }
}

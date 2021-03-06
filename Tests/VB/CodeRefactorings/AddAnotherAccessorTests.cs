using NUnit.Framework;
using RefactoringEssentials.VB.CodeRefactorings;

namespace RefactoringEssentials.Tests.VB.CodeRefactorings
{
    [TestFixture]
    public class AddAnotherAccessorTests : VBCodeRefactoringTestBase
    {
        [Test]
        public void TestAddSet()
        {
            Test<AddAnotherAccessorCodeRefactoringProvider>(@"
Class TestClass
    Dim _field As Integer
    Public ReadOnly Property $Field As Integer
        Get
            Return _field
        End Get
    End Property
End Class", @"
Class TestClass
    Dim _field As Integer
    Public Property Field As Integer
        Get
            Return _field
        End Get
        Set(ByVal value As Integer)
            _field = value
        End Set
    End Property
End Class");
        }

        [Test]
        public void TestAddSet_ReadOnlyField()
        {
            Test<AddAnotherAccessorCodeRefactoringProvider>(@"
Class TestClass
    ReadOnly _field As Integer
    Public ReadOnly Property $Field As Integer
        Get
            Return _field
        End Get
    End Property
End Class", @"
Class TestClass
    ReadOnly _field As Integer
    Public Property Field As Integer
        Get
            Return _field
        End Get
        Set(ByVal value As Integer)
            Throw New System.NotImplementedException()
        End Set
    End Property
End Class");
        }

        [Test]
        public void TestAddGet()
        {
            Test<AddAnotherAccessorCodeRefactoringProvider>(@"
Class TestClass
    Dim _field As Integer
    Public WriteOnly Property $Field As Integer
        Set(ByVal value As Integer)
            _field = value
        End Set
    End Property
End Class", @"
Class TestClass
    Dim _field As Integer
    Public Property Field As Integer
        Get
            Return _field
        End Get
        Set(ByVal value As Integer)
            _field = value
        End Set
    End Property
End Class");
        }

        [Test]
        public void TestAddGetWithComment()
        {
            Test<AddAnotherAccessorCodeRefactoringProvider>(@"
Class TestClass
    Dim _field As Integer
    Public WriteOnly Property $Field As Integer
        ' Some comment
        Set(ByVal value As Integer)
            _field = value
        End Set
    End Property
End Class", @"
Class TestClass
    Dim _field As Integer
    Public Property Field As Integer
        Get
            Return _field
        End Get
        ' Some comment
        Set(ByVal value As Integer)
            _field = value
        End Set
    End Property
End Class");
        }
    }
}
